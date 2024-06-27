const confirmButton = document.getElementById("confirm");
const cancelButton = document.getElementById("cancel");
const conformationWindow = document.getElementById("conformationWindow");
let _orderId = "";

function openConformationWindow(orderId) {
    conformationWindow.classList.remove("hidden");
    document.getElementById("reason").focus();
    _orderId = orderId;
}

confirmButton.addEventListener("click", () => {
    const reasonTextArea = document.getElementById("reason")
    const reason = reasonTextArea.value;
    if (reason.trim() === '') {
        reasonTextArea.style.backgroundColor = "#FFF5F5";
    } else {
        conformationWindow.classList.add("hidden");
        cancelOrder(reasonTextArea.value);
        reasonTextArea.style.backgroundColor = "#FFFFFF";
        document.getElementById("reason").value = "";
    }
});

cancelButton.addEventListener("click", () => {
    conformationWindow.classList.add("hidden");
});

function cancelOrder(reasonOfCanceled) {
    fetch(`http://localhost:5106/orders/client/cancel/${_orderId}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({reasonOfCanceled: reasonOfCanceled})
    })
        .then(response => {
            getOrders();
            console.log('Заказ отменен', response);
        })
        .catch(error => {
            alert('Произошла ошибка');
            console.error('Ошибка при отмене заказа:', reasonOfCanceled);
        });
}