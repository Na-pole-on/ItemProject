function row(item) {
    const col = document.createElement("div");
    col.className = "col-4";

    const border = document.createElement("div");
    border.className = "p-0 my-3 border rounded";

    const card = document.createElement("div");
    card.className = "card";

    const img = document.createElement("img");
    img.className = "card-img-top";
    img.setAttribute("src", item.photoUrl);

    const card_body = document.createElement("div");
    card_body.className = "card-body";

    const h3 = document.createElement("h3");
    h3.className = "card-title";
    h3.innerText = item.name;

    const p = document.createElement("p");
    p.className = "card-text";
    p.innerText = item.shortDescription;

    const row = document.createElement("div");
    row.className = "row";

    const price_col = document.createElement("div");
    price_col.className = "col pt-1";

    const price = document.createElement("span");
    price.style = "font-size:1.5rem;";
    price.innerHTML = "$" + item.price;
    price_col.append(price);

    const btn_col = document.createElement("div");
    btn_col.className = "col";

    const btn = document.createElement("button");
    btn.className = "btn btn-secondary form-control";
    btn.innerText = "Details";
    btn_col.append(btn);

    row.append(price_col);
    row.append(btn_col);

    card_body.append(h3);
    card_body.append(p);
    card_body.append(row);

    card.append(img);
    card.append(card_body);

    border.append(card);

    col.append(border);

    return col;
}