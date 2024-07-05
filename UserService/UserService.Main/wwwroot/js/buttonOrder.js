function createChatButton(row, order) {
    const chatCell = row.insertCell();
    chatCell.id = 'chatCell';
    const chatButton = document.createElement('button');
    chatButton.classList.add('btn', 'order-chat-button', 'action-button');
    chatButton.textContent = 'Чат';
    chatButton.onclick = () => openConformationWindowCancel(order.id);
    chatCell.appendChild(chatButton);
}

function createAcceptButton(row, order) {
    const acceptCell = row.insertCell();
    acceptCell.id = 'acceptCell';
    const acceptButton = document.createElement('button');
    acceptButton.classList.add('btn', 'order-accept-button', 'action-button');
    acceptButton.textContent = 'Принять';
    acceptButton.disabled = order.status !== 3;
    acceptButton.onclick = () => openConformationWindowAccept(order.id);
    acceptCell.appendChild(acceptButton);
}

function createCancelButton(row, order) {
    const cancelCell = row.insertCell();
    cancelCell.id = 'cancelCell';
    const cancelButton = document.createElement('button');
    cancelButton.classList.add('btn', 'close-button');
    const closeIcon = document.createElement('span');
    closeIcon.classList.add('close-icon');
    cancelButton.appendChild(closeIcon);
    cancelButton.onclick = () => openConformationWindowCancel(order.id);
    cancelCell.appendChild(cancelButton);
}