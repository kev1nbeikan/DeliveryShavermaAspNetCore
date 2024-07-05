function createChatButton(orderButton, order) {
    const chatButton = document.createElement('button');
    chatButton.id = "order-chat-button";
    chatButton.classList.add('btn', 'action-button');
    chatButton.textContent = 'Чат с клиентом';
    chatButton.onclick = () => {
        window.open(` http://localhost:5223/Chat/Room?RecipientId=${order.StoreId}&ChatName=Магазин`, '_blank'); // Open in a new window
    };
    orderButton.appendChild(chatButton);
}

function createAcceptButton(orderButton, order, status) {
    const acceptButton = document.createElement('button');
    acceptButton.id = "order-accept-button";
    acceptButton.classList.add('btn', 'action-button');
    if (status === 1) {
        acceptButton.textContent = 'Заказ забран';
    } else {
        acceptButton.textContent = 'Заказ доставлен';
    }
    acceptButton.disabled = status === 0;
    acceptButton.onclick = () => openConformationWindowAccept(order.id, status);
    orderButton.appendChild(acceptButton);
}

function createCancelButton(orderButton, order) {
    const cancelButton = document.createElement('button');
    cancelButton.id = "order-cancel-button";
    cancelButton.classList.add('btn', 'action-button');
    cancelButton.textContent = 'Отменить заказ';
    cancelButton.onclick = () => openConformationWindowCancel(order.id);
    orderButton.appendChild(cancelButton);
}

function deleteButton() {
    const orderButton = document.getElementById("order-button");
    orderButton.innerHTML = '';
    return orderButton;
}