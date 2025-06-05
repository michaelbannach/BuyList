let lastPurchases = [];

async function loadPurchases() {
    const response = await fetch('/api/purchase', { credentials: 'include' });
    const container = document.getElementById('purchaseList');

    if (!response.ok) {
        container.innerHTML = `<tr><td colspan="3">Fehler beim Laden!</td></tr>`;
        return;
    }

    lastPurchases = await response.json();

    if (!lastPurchases || lastPurchases.length === 0) {
        container.innerHTML = `<tr><td colspan="3">Noch keine Einkäufe getätigt.</td></tr>`;
        return;
    }

    renderPurchases(lastPurchases);
}

function renderPurchases(purchases) {
    const sortValue = document.getElementById('sortSelect')?.value || "date-desc";

    const sorted = [...purchases].sort((a, b) => {
        switch (sortValue) {
            case "date-asc": return new Date(a.purchaseDate) - new Date(b.purchaseDate);
            case "date-desc": return new Date(b.purchaseDate) - new Date(a.purchaseDate);
            case "price-asc": return a.price - b.price;
            case "price-desc": return b.price - a.price;
            default: return 0;
        }
    });

    const container = document.getElementById('purchaseList');
    container.innerHTML = "";

    if (sorted.length === 0) {
        container.innerHTML = `<tr><td colspan="3">Noch keine Einkäufe getätigt.</td></tr>`;
        return;
    }

    sorted.forEach(p => {
        const row = document.createElement('tr');

        const dateCell = document.createElement('td');
        dateCell.textContent = new Date(p.purchaseDate).toLocaleDateString('de-DE');

        const userCell = document.createElement('td');
        userCell.textContent = p.userId || "Unbekannt";

        const priceCell = document.createElement('td');
        priceCell.textContent = `${(p.price ?? 0).toFixed(2).replace('.', ',')} €`;

        row.appendChild(dateCell);
        row.appendChild(userCell);
        row.appendChild(priceCell);

        container.appendChild(row);
    });
}

window.addEventListener('DOMContentLoaded', () => {
    loadPurchases();
    const sortSelect = document.getElementById('sortSelect');
    if (sortSelect) {
        sortSelect.addEventListener('change', () => renderPurchases(lastPurchases));
    }
});
