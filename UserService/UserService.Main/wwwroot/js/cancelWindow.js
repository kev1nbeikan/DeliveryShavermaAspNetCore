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
    fetch(`http://localhost:5106/orders/client/cancel/${_orderIdCancel}`, {
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
            console.log('Заказ отменен', response);
        })
        .catch(error => {
            alert('Произошла ошибка');
            console.error('Ошибка при отмене заказа:', reasonOfCanceledText);
        });
}