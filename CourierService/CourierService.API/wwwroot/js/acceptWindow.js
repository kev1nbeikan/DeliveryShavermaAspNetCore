const confirmButtonAccept = document.getElementById("confirmAccept");
const cancelButtonAccept = document.getElementById("cancelAccept");
const conformationWindowAccept = document.getElementById("conformationWindowAccept");
let _orderIdAccept = "";
let _status;

function openConformationWindowAccept(orderId, status) {
    conformationWindowAccept.classList.remove("hidden");
    document.getElementById("reason").focus();
    _orderIdAccept = orderId;
    _status = status;
}

confirmButtonAccept.addEventListener("click", () => {
    conformationWindowAccept.classList.add("hidden");
    if (_status === 1) deliveryOrder();
    if (_status === 2) acceptOrder();
});

cancelButtonAccept.addEventListener("click", () => {
    conformationWindowAccept.classList.add("hidden");
});

function deliveryOrder() {
    fetch(`http://localhost:5106/orders/courier/delivering/${_orderIdAccept}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'UserId': AuthHeaders.UserId,
            'Role': AuthHeaders.Role,
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Не удалось отменить заказ');
            }
            getCurrentOrders();
            showInfoMessage("Закакз принят");
            console.log('Заказ принят!', response);
        })
        .catch(error => {
            showErrorMessage("Произошла ошибка")
            console.error('Ошибка при отправке запроса на прием:', error);
        });
}

function acceptOrder() {
    fetch(`http://localhost:5106/orders/courier/waitingClient/${_orderIdAccept}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json',
            'UserId': AuthHeaders.UserId,
            'Role': AuthHeaders.Role,
        }
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Не удалось отменить заказ');   
            }
            getCurrentOrders();
            showInfoMessage("Закакз доставлен");
            console.log('Заказ принят!', response);
        })
        .catch(error => {
            showErrorMessage("Произошла ошибка")
            console.error('Ошибка при отправке запроса на прием:', error);
        });
}