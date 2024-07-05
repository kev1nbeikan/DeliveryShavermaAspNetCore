const confirmButtonCancel = document.getElementById("confirmCancel");
const cancelButtonCancel = document.getElementById("cancelCancel");
const conformationWindowCancel = document.getElementById("conformationWindowCancel");
let _orderIdCancel = "";

function openConformationWindowCancel(orderId) {
    conformationWindowCancel.classList.remove("hidden");
    document.getElementById("reason").focus();
    _orderIdCancel = orderId;
}

confirmButtonCancel.addEventListener("click", () => {
    const reasonTextArea = document.getElementById("reason")
    const reason = reasonTextArea.value;
    if (reason.trim() === '') {
        reasonTextArea.style.backgroundColor = "#FFF5F5";
    } else {
        conformationWindowCancel.classList.add("hidden");
        cancelOrder(reasonTextArea.value);
        reasonTextArea.style.backgroundColor = "#FFFFFF";
        document.getElementById("reason").value = "";
    }
});

cancelButtonCancel.addEventListener("click", () => {
    conformationWindowCancel.classList.add("hidden");
});

function cancelOrder(reasonOfCanceledText) {
    fetch(`http://localhost:5106/orders/courier/cancel/${_orderIdCancel}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'UserId': AuthHeaders.UserId,
            'Role': AuthHeaders.Role,
        },
        body: JSON.stringify({reasonOfCanceled: reasonOfCanceledText})
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Не удалось отменить заказ');
            }
            getCurrentOrders();
            showInfoMessage("Заказ отменен");
            console.log('Заказ отменен', response);
        })
        .catch(error => {
            showErrorMessage("Произошла ошибка, вы не можете сейчас отменить заказ")
            console.error('Ошибка при отмене заказа:', reasonOfCanceledText);
        });
}