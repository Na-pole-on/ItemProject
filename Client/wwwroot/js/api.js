async function getItems() {
    const response = await fetch("/Items/GetAll", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });

    if (response.ok === true) {
        const items = await response.json();
        const rows = document.querySelector(".items-list");

        items.forEach(item => rows.append(row(item)));
        btnShowMore(0);
    }
    else {
        notFound();
    }
}

getItems();