function updateCartCount() {
    fetch('/Cart/GetCartItems')
        .then(response => response.json())
        .then(cart => {
            const totalQuantity = cart.reduce((total, item) => total + item.quantity, 0);
            document.getElementById('cart-item-count').textContent = totalQuantity;
        })
        .catch(error => {
            console.error('Error fetching cart count:', error);
        });
}

document.addEventListener('DOMContentLoaded', function() {
    updateCartCount();
});
