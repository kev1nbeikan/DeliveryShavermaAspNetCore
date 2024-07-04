let ordersToCheck = [];

let currentTable = 'CurrentOrdersTable'

const AuthHeaders = {
    'UserId': getCookie('UserId'),
    'Role': getCookie('Role')
};

async function getCurrentOrders() {
    try {
        const response = await fetch('http://localhost:5106/orders/courier', {
            headers: AuthHeaders
        });
        if (response.status === 200) {
            const data = await response.json();
            displayCurrentOrders(data);
            ordersToCheck = [data.id];
        } else if (response.status === 204) {
            console.info('Заказов нет');
            displayError("Текущих заказов нету", currentTable);
            await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
        }
    } catch (error) {
        console.error('Ошибка при получении данных:', error);
        displayError("Ошибка при подключению к серверу", currentTable);
        await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_ERROR);
    }
}


function displayCurrentOrders(order) {
    const ordersTable = document.getElementById(currentTable)
        .getElementsByTagName('tbody')[0];

    ordersTable.innerHTML = '';

    const row = ordersTable.insertRow();

    const statusCell = row.insertCell();
    statusCell.id = 'statusCell';
    statusCell.textContent = StatusMapping[order.status];

    const basketCell = row.insertCell();
    basketCell.id = 'basketCell';
    order.basket.forEach(item => {
        const listItem = document.createElement('li');
        listItem.textContent = `${item.name}, ${item.amount}, ${item.price} рублей`;
        basketCell.appendChild(listItem);
    });

    row.insertCell().textContent = order.comment;
    row.insertCell().textContent = order.storeAddress;
    row.insertCell().textContent = order.clientAddress;
    row.insertCell().textContent = order.clientNumber;
    row.insertCell().textContent = order.deliveryTime;
    row.insertCell().textContent = order.cookingTime;

    displayButtons(order);
}

function displayButtons(order) {

    const mainDiv = document.querySelector('main');

    const orderButton = document.createElement('div');
    orderButton.id = 'order-button'; // Добавляем id для идентификации

    const chatButton = document.createElement('button');
    chatButton.id = "order-chat-button";
    chatButton.classList.add('btn', 'action-button');
    chatButton.textContent = 'Чат с клиентом';
    chatButton.onclick = () => openConformationWindowCancel(order.id);
    orderButton.appendChild(chatButton);


    const acceptButton = document.createElement('button');
    acceptButton.id = "order-accept-button";
    acceptButton.classList.add('btn', 'action-button');
    if (order.status === 1) {
        acceptButton.textContent = 'Заказ забран';
        acceptButton.onclick = () => openConformationWindowAccept(order.id, order.status);
    } else {
        acceptButton.textContent = 'Заказ доставлен';
        acceptButton.onclick = () => openConformationWindowAccept(order.id, order.status);
    }
    acceptButton.disabled = order.status === 0;
    acceptButton.onclick = () => openConformationWindowCancel(order.id);
    orderButton.appendChild(acceptButton);


    const cancelButton = document.createElement('button');
    cancelButton.id = "order-cancel-button";
    cancelButton.classList.add('btn', 'action-button');
    cancelButton.textContent = 'Отменить заказ';
    cancelButton.onclick = () => openConformationWindowCancel(order.id);
    orderButton.appendChild(cancelButton);

    mainDiv.appendChild(orderButton);

    //
    // const chatButton = document.createElement('button');
    // chatButton.classList.add('btn', 'order-chat-button', 'action-button');
    // chatButton.textContent = 'Чат';
    // chatButton.onclick = () => openConformationWindowCancel(order.id);
    // actionsCell.appendChild(chatButton);
    //
    // const acceptCell = row.insertCell();
    // acceptCell.id = 'acceptCell';
    //
    // const acceptButton = document.createElement('button');
    // acceptButton.classList.add('btn', 'order-accept-button', 'action-button');
    // if (order.status === 1) {
    //     acceptButton.textContent = 'Принят';
    //     acceptButton.onclick = () => openConformationWindowAccept(order.id, order.status);
    // } else {
    //     acceptButton.textContent = 'Доставлен';
    //     acceptButton.onclick = () => openConformationWindowAccept(order.id, order.status);
    // }
    // acceptButton.disabled = order.status === 0;
    // acceptCell.appendChild(acceptButton);
    //
    //
    // const cancelCell = row.insertCell();
    // cancelCell.id = 'cancelCell';
    //
    // const cancelButton = document.createElement('button');
    // cancelButton.classList.add('btn', 'close-button');
    // const closeIcon = document.createElement('span');
    // closeIcon.classList.add('close-icon');
    // cancelButton.appendChild(closeIcon);
    // cancelButton.onclick = () => openConformationWindowCancel(order.id);
    // cancelCell.appendChild(cancelButton);
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
                await getCurrentOrders();
            }
        } catch (error) {
            console.error(`Ошибка при проверке статуса заказа ${orderId}:`, error);
            await getCurrentOrders();
        }
    });
    await Promise.all(promises);
}

async function updateOrderStatus(orderId, newStatus) {
    const ordersTable = document.getElementById(currentTable).getElementsByTagName('tbody')[0];
    const rows = ordersTable.querySelectorAll('tr');
    const index = ordersToCheck.findIndex(id => id === orderId);
    const orderRow = rows[index];

    if (StatusMapping[newStatus] === orderRow.cells[0].textContent) {
        return;
    }
    if (newStatus !== "0" || newStatus !== "1" || newStatus !== "2") {
        await getCurrentOrders();
    }
    if (orderRow) {
        orderRow.cells[0].textContent = StatusMapping[newStatus];
    }
}

async function restartCurrentOrderPage(time) {
    await new Promise((resolve) => {
        ordersToCheck = [];
        setTimeout(() => {
            getCurrentOrders();
            resolve();
        }, time);
    });
}
