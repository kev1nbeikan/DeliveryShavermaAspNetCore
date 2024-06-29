let ordersToCheck = [];

let currentTable = 'CurrentOrdersTable'

const AuthHeaders = {
    'UserId': getCookie('UserId'),
    'Role': getCookie('Role')
};

async function getCurrentOrders() {
    try {
        const response = await fetch('http://localhost:5106/orders/client/current', {
            headers: AuthHeaders
        });
        if (response.status === 200) {
            const data = await response.json();
            displayCurrentOrders(data);
            ordersToCheck = data.map(order => order.id);
        } else if (response.status === 204) {
            console.error('Заказов нет');
            displayError("Текущих заказов нету 😓", currentTable);
            await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
        }
    } catch (error) {
        console.error('Ошибка при получении данных:', error);
        displayError("Ошибка при подключению к серверу", currentTable);
        await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_ERROR);
    }
}


function displayCurrentOrders(orders) {
    const ordersTable = document.getElementById(currentTable)
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
            let statusCell = document.getElementById('statusCell');
            statusCell.style.backgroundColor = '#e3ffe3';
            
            const acceptButton = document.createElement('button');
            acceptButton.classList.add('btn', 'btn-success', 'order-accept-button');
            acceptButton.textContent = 'Принять';
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
                console.error(`Заказ не найден ${orderId}`, response);
                await getCurrentOrders();
            } else {
                console.error(`Ошибка при получении статуса заказа ${orderId}`, response);
                await getCurrentOrders();
            }
        } catch (error) {
            console.error(`Ошибка при проверке статуса заказа ${orderId}:`, error);
            await getCurrentOrders();
        }
    });
    await Promise.all(promises);
}

function updateOrderStatus(orderId, newStatus) {
    const ordersTable = document.getElementById(currentTable).getElementsByTagName('tbody')[0];
    const rows = ordersTable.querySelectorAll('tr');
    const index = ordersToCheck.findIndex(id => id === orderId);
    const orderRow = rows[index];

    if (StatusMapping[newStatus] === orderRow.cells[0].textContent) {
        return;
    }
    if (newStatus === "3") {
        getCurrentOrders();
    }
    if (orderRow) {
        orderRow.cells[0].textContent = StatusMapping[newStatus];
    }
}

async function restartCurrentOrderPage(time){
    await new Promise((resolve) => {
        ordersToCheck = [];
        setTimeout(() => {
            getCurrentOrders();
            resolve();
        }, time);
    });
}
