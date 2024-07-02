// let currentTable = 'CurrentStoreOrdersTable'
// let serverWorking = false;
// let newestOrderDate;
//
// const AuthHeaders = {
//     'UserId': getCookie('StoreId'),
//     'Role': getCookie('Role')
// };
//
// async function getStoreOrders() {
//     try {
//         const response = await fetch('http://localhost:5106/orders/store', {
//             headers: AuthHeaders
//         });
//         if (response.status === 200) {
//             const data = await response.json();
//             const ordersTable = document.getElementById(currentTable)
//                 .getElementsByTagName('tbody')[0];
//             ordersTable.innerHTML = '';
//             displayCurrentOrders(data);
//         } else if (response.status === 204) {
//             console.error('Заказов нет');
//             serverWorking = false;
//             displayError("Заказов нет", currentTable);
//             await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_NO_CONTENT);
//         }
//     } catch (error) {
//         console.error('Ошибка при получении данных:', error);
//         serverWorking = false;
//         displayError("Ошибка при подключению к серверу", currentTable);
//         await restartCurrentOrderPage(SERVER_CHECK_INTERVAL_ERROR);
//     }
// }
//
//
// function displayCurrentOrders(orders) {
//     const ordersTable = document.getElementById(currentTable)
//         .getElementsByTagName('tbody')[0];
//    
//     orders.forEach(order => {
//         const row = ordersTable.insertRow();
//
//         const statusCell = row.insertCell();
//         statusCell.id = 'statusCell';
//         statusCell.textContent = StatusMapping[order.status];
//
//         const basketCell = row.insertCell();
//         basketCell.id = 'basketCell';
//         order.basket.forEach(item => {
//             const listItem = document.createElement('li');
//             listItem.textContent = `${item.name}, ${item.amount} штук, ${item.price} рублей`;
//             basketCell.appendChild(listItem);
//         });
//
//         row.insertCell().textContent = order.comment;
//         row.insertCell().textContent = order.courierNumber;
//         row.insertCell().textContent = order.cookingTime;
//
//         if (!newestOrderDate || new Date(order.orderDate) > new Date(newestOrderDate)) {
//             newestOrderDate = order.orderDate;
//         }
//        
//         serverWorking = true;
//     });
// }
// async function checkNewOrder() {
//     if (serverWorking) {
//         try {
//             const response = await fetch(
//                 `http://localhost:5106/orders/store/getNewOrders/${newestOrderDate}`, {
//                     headers: AuthHeaders
//                 });
//             if (response.status === 200) {
//                 const data = await response.json();
//                 displayCurrentOrders(data);
//             } else if (response.status === 204) {
//                 console.info('Новых заказов нет');
//             }
//         } catch (error) {
//             console.error('Ошибка при получении данных:', error);
//             await getStoreOrders();
//         }
//     }
// }
//
// async function restartCurrentOrderPage(time) {
//     await new Promise((resolve) => {
//         ordersToCheck = [];
//         setTimeout(() => {
//             getStoreOrders();
//             resolve();
//         }, time);
//     });
// }
//
// function displayError(message, orderTable) {
//     const ordersTable = document.getElementById(orderTable).getElementsByTagName('tbody')[0];
//     ordersTable.innerHTML = '';
//
//     const row = ordersTable.insertRow();
//     const cell = row.insertCell();
//     cell.colSpan = 10;
//     cell.textContent = message;
//     cell.style.fontSize = '20px';
//     cell.style.padding = '20px';
// }