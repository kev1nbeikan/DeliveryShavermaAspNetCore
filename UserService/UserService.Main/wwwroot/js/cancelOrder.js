const RoleMapping = {
    "0": "Клиент",
    "1": "Магазин",
    "2": "Курьер",
    "3": "Администратор"
};

let cancelTable = 'CancelOrdersTable';

async function getCancelOrders() {
    try {
        const response = await fetch('http://localhost:5106/orders/client/canceled', {
            headers: AuthHeaders
        });
        if (response.status === 200) {
            const data = await response.json();
            displayCancelOrders(data);
        } else if (response.status === 204) {
            console.error('Заказов нет');
            displayError("Отмененных заказов нету 😊", cancelTable);
            await restartCancelOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
        }
    } catch (error) {
        console.error('Ошибка при получении данных:', error);
        displayError("Ошибка при подключению к серверу", cancelTable);
        await restartCancelOrderPage(SERVER_CHECK_INTERVAL_ERROR);
    }
}

function displayCancelOrders(orders) {
    const ordersTable = document.getElementById(cancelTable)
        .getElementsByTagName('tbody')[0];

    ordersTable.innerHTML = '';

    orders.forEach(order => {
        const row = ordersTable.insertRow();

        row.insertCell().textContent = order.price;
        row.insertCell().textContent = order.comment;

        let _orderDate = new Date(order.orderDate);
        row.insertCell().textContent = _orderDate.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });

        let _cancelDate = new Date(order.canceledDate);
        row.insertCell().textContent = _cancelDate.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
        
        // row.insertCell().textContent = order.orderDate;
        row.insertCell().textContent = StatusMapping[order.lastStatus];
        row.insertCell().textContent = `${RoleMapping[order.whoCanceled]}: ${order.reasonOfCanceled}`;

        const basketCell = row.insertCell();
        basketCell.id = 'basketCell';
        order.basket.forEach(item => {
            const listItem = document.createElement('li');
            listItem.textContent = `${item.name}, ${item.amount} штук, ${item.price} рублей`;
            basketCell.appendChild(listItem);
        });
    });
}

async function restartCancelOrderPage(time){
    await new Promise((resolve) => {
        ordersToCheck = [];
        setTimeout(() => {
            getCancelOrders();
            resolve();
        }, time);
    });
}