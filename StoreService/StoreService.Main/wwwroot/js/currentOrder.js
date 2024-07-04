let currentTable = 'CurrentStoreOrdersTable'
let serverWorking = false;
let newestOrderDate;
let ordersToCheck = [];

const AuthHeaders = {
    'UserId': getCookie('UserId'),
    'Role': getCookie('Role')
};

async function getCurrentStoreOrders() {
    try {
        const response = await fetch('http://localhost:5106/orders/store', {
            headers: AuthHeaders
        });
        if (response.status === 200) {
            const data = await response.json();
            const ordersTable = document.getElementById(currentTable)
                .getElementsByTagName('tbody')[0];
            ordersTable.innerHTML = '';
            displayCurrentOrders(data);
            ordersToCheck = data.map(order => order.id);
        } else if (response.status === 204) {
            console.error('Заказов нет');
            serverWorking = false;
            displayError("Заказов нет", currentTable);
            await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
        }
    } catch (error) {
        console.error('Ошибка при получении данных:', error);
        serverWorking = false;
        displayError("Ошибка при подключению к серверу", currentTable);
        await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_ERROR);
    }
}


function displayCurrentOrders(orders) {
    const ordersTable = document.getElementById(currentTable)
        .getElementsByTagName('tbody')[0];

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
        row.insertCell().textContent = order.courierNumber;
        row.insertCell().textContent = order.cookingTime;

        if (!newestOrderDate || new Date(order.orderDate) > new Date(newestOrderDate)) {
            newestOrderDate = order.orderDate;
        }

        displayButtons(row, order);

        serverWorking = true;
    });
}

function displayButtons(row, order){
    const actionsCell = row.insertCell();
    actionsCell.id = 'actionCell';

    const chatButton = document.createElement('button');
    chatButton.classList.add('btn', 'order-chat-button', 'action-button');
    chatButton.textContent = 'Чат';
    chatButton.onclick = () => openConformationWindowCancel(order.id);
    actionsCell.appendChild(chatButton);
    
    if (order.status === 0) {
        const acceptButton = document.createElement('button');
        acceptButton.classList.add('btn', 'order-accept-button');
        acceptButton.textContent = 'Готов';
        acceptButton.onclick = () => openConformationWindowAccept(order.id);
        actionsCell.appendChild(acceptButton);

        const cancelCell = row.insertCell();
        cancelCell.id ='cancelCell';

        const cancelButton = document.createElement('button');
        cancelButton.classList.add('btn', 'close-button');
        const closeIcon = document.createElement('span');
        closeIcon.classList.add('close-icon');
        cancelButton.appendChild(closeIcon);
        cancelButton.onclick = () => openConformationWindowCancel(order.id);
        cancelCell.appendChild(cancelButton);
    }
}

async function checkNewOrder() {
    if (serverWorking) {
        try {
            const response = await fetch(
                `http://localhost:5106/orders/store/getNewOrders/${newestOrderDate}`, {
                    headers: AuthHeaders
                });
            if (response.status === 200) {
                const data = await response.json();
                displayCurrentOrders(data);
            } else if (response.status === 204) {
                console.info('Новых заказов нет');
            }
        } catch (error) {
            console.error('Ошибка при получении данных:', error);
            await getCurrentStoreOrders();
        }
    }
}

async function checkOrderStatus() {
    const promises = ordersToCheck.map(async (orderId) => {
        try {
            const response = await fetch(`http://localhost:5106/orders/status/${orderId}`, {
                headers: AuthHeaders
            });
            if (response.status === 200) {
                const orderData = await response.text();
                await updateOrderStatus(orderId, orderData);
            } else if (response.status === 204) {
                console.error(`Заказ не найден ${orderId}`, response);
                await getCurrentStoreOrders();
            }
        } catch (error) {
            console.error(`Ошибка при проверке статуса заказа ${orderId}:`, error);
            await getCurrentStoreOrders();
        }
    });
    await Promise.all(promises);
}

async function updateOrderStatus(orderId, newStatus) {
    const ordersTable = document.getElementById(currentTable).getElementsByTagName('tbody')[0];
    const rows = ordersTable.querySelectorAll('tr');
    const index = ordersToCheck.findIndex(id => id === orderId);
    const orderRow = rows[index];

    if (newStatus !== "0" || newStatus !== "1") {
       await getCurrentOrders();
    }
    if (StatusMapping[newStatus] === orderRow.cells[0].textContent) {
        return;
    }
    if (orderRow) {
        orderRow.cells[0].textContent = StatusMapping[newStatus];
    }
}

async function restartCurrentOrderPage(time) {
    await new Promise((resolve) => {
        ordersToCheck = [];
        setTimeout(() => {
            getCurrentStoreOrders();
            resolve();
        }, time);
    });
}

