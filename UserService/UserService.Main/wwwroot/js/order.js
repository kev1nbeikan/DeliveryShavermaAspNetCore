const STATUS_CHECK_INTERVAL = 5000;
const SERVER_CHECK_INTERVAL = 5000;

let ordersToCheck = [];

const StatusMapping = {
    "0": "Готовится",
    "1": "Ожидает курьра",
    "2": "Доставляется",
    "3": "Прибыл к клиенту"
};

const AuthHeaders = {
    'UserId': getCookie('UserId'),
    'Role': getCookie('Role')
};

function getOrders() {
    fetch('http://localhost:5106/orders/client/current', {
            headers: AuthHeaders
        }
    )
        .then(response => {
            if (response.status === 200) {
                return response.json();
            } else if (response.status === 204) {
                throw new Error("204");
            } else {
                throw new Error();
            }
        })
        .then(data => {
            displayOrders(data);
            ordersToCheck = data.map(order => order.id);
        })
        .catch(error => {
            console.error('Ошибка при получении данных:', error);
            if (error.message === "204") {
                displayError("Текущих заказов нету 😓");
            }else{
                displayError("Ошибка при подключению к серверу");
            }
        });
}

function displayOrders(orders) {
    const ordersTable = document.getElementById('ordersTable')
        .getElementsByTagName('tbody')[0];

    ordersTable.innerHTML = '';

    orders.forEach(order => {
        const row = ordersTable.insertRow();

        const statusCell = row.insertCell();
        statusCell.id = 'statusCell';
        statusCell.textContent = StatusMapping[order.status];
        
        const basketCell = row.insertCell();
        basketCell.id = 'basketCell';
        order.basket.forEach(item => {
            const listItem = document.createElement('li');
            listItem.textContent = `${item.name}, ${item.amount} штук, ${item.price} рублей`;
            basketCell.appendChild(listItem);
        });
        
        row.insertCell().textContent = order.comment;
        row.insertCell().textContent = order.clientAddress;
        row.insertCell().textContent = order.courierNumber;
        row.insertCell().textContent = order.clientNumber;
        row.insertCell().textContent = order.price;

        const actionsCell = row.insertCell();
        actionsCell.classList.add('actions-cell');
        
        const cancelButton = document.createElement('button');
        cancelButton.classList.add('btn', 'btn-secondary', 'order-cancel-button');
        cancelButton.textContent = 'Отменить';
        cancelButton.onclick = () => openConformationWindowCancel(order.id);
        actionsCell.appendChild(cancelButton);
        
        if (order.status === 3) {
            const acceptButton = document.createElement('button');
            acceptButton.classList.add('btn', 'btn-success', 'order-accept-button');
            acceptButton.textContent = 'Принять';
            // acceptButton.onclick = () => openConformationWindowAccept(order.id);
            acceptButton.onclick = () => openConformationWindowAccept(order.id);
            actionsCell.appendChild(acceptButton);
        }
    });
}

async function checkOrderStatus() {
    const promises = ordersToCheck.map(async (orderId) => {
        try {
            const response = await fetch(`http://localhost:5106/orders/status/${orderId}`, {
                headers: AuthHeaders
            });
            if (response.status === 200) {
                const orderData = await response.text();
                updateOrderStatus(orderId, orderData);
            } else if (response.status === 204) {
                getOrders();
            } else {
                console.error(`Ошибка при получении статуса заказа ${orderId}`, response);
            }
        } catch (error) {
            console.error(`Ошибка при проверке статуса заказа ${orderId}:`, error);
        }
    });
    await Promise.all(promises);
}

function updateOrderStatus(orderId, newStatus) {
    const ordersTable = document.getElementById('ordersTable').getElementsByTagName('tbody')[0];
    const rows = ordersTable.querySelectorAll('tr');
    const orderRow = Array.from(rows).find(row => row.cells[0].textContent === orderId.toString());
    if (orderRow) {
        orderRow.cells[1].textContent = StatusMapping[newStatus];
    }
}

function displayError(message) {
    const ordersTable = document.getElementById('ordersTable').getElementsByTagName('tbody')[0];
    ordersTable.innerHTML = '';

    const row = ordersTable.insertRow();
    const cell = row.insertCell();
    cell.colSpan = 10;
    cell.textContent = message;
}

function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return cookieValue ? cookieValue[2] : null;
}