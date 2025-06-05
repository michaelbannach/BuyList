const itemInput = document.getElementById('itemInput');
const amountInput = document.getElementById('amountInput');
const button = document.getElementById('add');
const list = document.getElementById('list');
const purchaseButton = document.getElementById('purchase');

const priceInputSection = document.getElementById('priceInputSection');
const priceInput = document.getElementById('priceInput');
const confirmButton = document.getElementById('confirmPurchase');

let items = [];

async function loadItems() {
    const response = await fetch('/api/item', { credentials: 'include' });
    if (response.ok) {
        items = await response.json();
        renderList();
    }
}

async function addItem(name, amount) {
    const response = await fetch('/api/item', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ name, amount, done: false }),
        credentials: 'include'
    });
    if (response.ok) {
        const newItem = await response.json();
        items.push(newItem);
        renderList();
    }
}

async function toggleDone(item) {
    const response = await fetch(`/api/item/${item.id}`, {
        method: 'PATCH',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ done: !item.done }),
        credentials: 'include'
    });
    if (response.ok) {
        const updatedItem = await response.json();
        items = items.map(i => i.id === updatedItem.id ? updatedItem : i);
        renderList();
    }
}

async function deleteItem(id) {
    const response = await fetch(`/api/item/${id}`, {
        method: 'DELETE',
        credentials: 'include'
    });
    if (response.ok) {
        items = items.filter(i => i.id !== id);
        renderList();
    }
}

function renderList() {
    list.innerHTML = "";

    items.forEach((item) => {
        const li = document.createElement('li');

        const text = document.createElement('span');
        text.className = 'item-text';
        text.textContent = `${item.name} ${item.amount}`;
        text.style.textDecoration = item.done ? "line-through" : "none";

        const actions = document.createElement('div');
        actions.className = 'actions';

        const doneButton = document.createElement('button');
        doneButton.textContent = "Erledigt";
        doneButton.addEventListener('click', () => toggleDone(item));

        const deleteButton = document.createElement('button');
        deleteButton.textContent = "Entfernen";
        deleteButton.addEventListener('click', () => deleteItem(item.id));

        actions.appendChild(doneButton);
        actions.appendChild(deleteButton);

        li.appendChild(text);
        li.appendChild(actions);
        list.appendChild(li);
    });
}

button.addEventListener('click', async () => {
    const name = itemInput.value.trim();
    const amount = amountInput.value.trim();
    if (name === "" || amount === "") return;

    await addItem(name, amount);
    itemInput.value = "";
    amountInput.value = "";
});

purchaseButton.addEventListener('click', () => {
    const doneIds = items.filter(i => i.done).map(i => i.id);
    if (doneIds.length === 0) {
        alert("Keine erledigten Items zum Abschließen!");
        return;
    }
    priceInputSection.style.display = 'block';
    priceInput.focus();
});

confirmButton.addEventListener('click', async () => {
    const doneIds = items.filter(i => i.done).map(i => i.id);
    const price = parseFloat(priceInput.value.replace(',', '.'));

    if (isNaN(price) || price <= 0) {
        alert("Bitte gültigen Betrag eingeben.");
        return;
    }

    const response = await fetch('/api/purchase', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        credentials: 'include',
        body: JSON.stringify({
            itemIds: doneIds,
            price: price
        })
    });

    if (response.ok) {
        items = items.filter(i => !doneIds.includes(i.id));
        renderList();
        alert("Einkauf gespeichert!");
        priceInput.value = "";
        priceInputSection.style.display = 'none';
    } else {
        const errorText = await response.text();
        console.error("Fehler beim Einkauf:", errorText);
        alert("Fehler beim Speichern:\n" + errorText);
    }
});



window.addEventListener('DOMContentLoaded', () => {
    loadItems();
   
});
