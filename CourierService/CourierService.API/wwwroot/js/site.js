const STATUS_CHECK_INTERVAL = 5000;
const SERVER_CHECK_INTERVAL_ERROR = 15000;
const SERVER_CHECK_INTERVAL_NO_CONTENT = 7500;

const StatusMapping = {
    "0": "Готовится",
    "1": "Ожидает курьра",
    "2": "Доставляется",
    "3": "Прибыл к клиенту"
};

function getCookie(name) {
    const cookieValue = document.cookie.match('(^|;) ?' + name + '=([^;]*)(;|$)');
    return cookieValue ? cookieValue[2] : null;
}

function displayError(message, orderTable) {
    const ordersTable = document.getElementById(orderTable).getElementsByTagName('tbody')[0];
    ordersTable.innerHTML = '';

    const row = ordersTable.insertRow();
    const cell = row.insertCell();
    cell.colSpan = 12;
    cell.textContent = message;
    cell.style.fontSize = '20px';
    cell.style.padding = '20px';
}

const currentNav = document.getElementById("currentNav");
const lastNav = document.getElementById("lastNav");
const cancelNav = document.getElementById("cancelNav");

const currentUrl = window.location.pathname;
if (currentUrl.includes("/current")) {
    currentNav.classList.add("active");
} else if (currentUrl.includes("/last")) {
    lastNav.classList.add("active");
} else if (currentUrl.includes("/cancel")) {
    cancelNav.classList.add("active");
}