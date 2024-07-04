let lastTable = 'LastOrdersTable'

async function getLastOrders() {
    try {
        const response = await fetch('http://localhost:5106/orders/courier/last', {
            headers: AuthHeaders
        });
        if (response.status === 200) {
            const data = await response.json();
            displayLastOrders(data);
        } else if (response.status === 204) {
            console.error('Заказов нет');
            displayError("Прошлых заказов нету 😓", lastTable);
            await restartLastOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
        }
    } catch (error) {
        console.error('Ошибка при получении данных:', error);
        displayError("Ошибка при подключению к серверу", lastTable);
        await restartLastOrderPage(SERVER_CHECK_INTERVAL_ERROR);
    }
}

function displayLastOrders(orders) {
    const ordersTable = document.getElementById(lastTable)
        .getElementsByTagName('tbody')[0];

    ordersTable.innerHTML = '';

    orders.forEach(order => {
        const row = ordersTable.insertRow();

        
        let _orderDate = new Date(order.orderDate);
        row.insertCell().textContent = _orderDate.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
        let _deliveryDate = new Date(order.deliveryDate);
        row.insertCell().textContent = _deliveryDate.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });
        let _cookingDate = new Date(order.cookingDate);
        row.insertCell().textContent = _cookingDate.toLocaleDateString('ru-RU', {
            year: 'numeric',
            month: 'long',
            day: 'numeric',
            hour: '2-digit',
            minute: '2-digit'
        });

        row.insertCell().textContent = order.deliveryTime;
        row.insertCell().textContent = order.comment;
        
        const basketCell = row.insertCell();
        basketCell.id = 'basketCell';
        order.basket.forEach(item => {
            const listItem = document.createElement('li');
            listItem.textContent = `${item.name}, ${item.amount}, ${item.price} рублей`;
            basketCell.appendChild(listItem);
        });
    });
}

async function restartLastOrderPage(time){
    await new Promise((resolve) => {
        ordersToCheck = [];
        setTimeout(() => {
            getLastOrders();
            resolve();
        }, time);
    });
}