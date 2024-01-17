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
    btn.addEventListener("click", async () => await details(item.id));
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

function emptyCart(cart) {
    var card = document.getElementsByClassName("cart-list");

    var card_box = document.createElement("div");
    card_box.className = "card cart-box overflow-auto mb-4";

    var cart_header = document.createElement("div");
    cart_header.className = "card-header py-3";

    var h5 = document.createElement("h5");
    h5.className = "mb-0";
    h5.innerText = "Cart - 0 items";

    cart_header.append(h5);
    card_box.append(cart_header);

    var empty = document.createElement("div");
    empty.className = "text-center p-5";
    empty.innerText = "Your shopping cart is empty, add something to it!";

    card_box.append(empty);
    card[0].append(card_box);

    addSummary(cart);
}

function addSummary(cart) {
    var summary = document.getElementsByClassName("cart-summary");

    var card = document.createElement("div");
    card.className = "card mb-4";

    var card_header = document.createElement("div");
    card_header.className = "card-header py-3";

    var h5 = document.createElement("h5");
    h5.className = "mb-0";
    h5.innerText = "Summary";

    card_header.append(h5);
    card.append(card_header);

    var card_body = document.createElement("div");
    card_body.className = "card-body";

    var list_group = document.createElement("ul");
    list_group.className = "list-group list-group-flush";

    var li_products = document.createElement("li");
    li_products.className = "list-group-item d-flex justify-content-between align-items-center border-0 px-0 pb-0";
    li_products.innerText = "Products";

    var span_price = document.createElement("span");
    span_price.setAttribute("id", "quentity-products");
    span_price.innerText = cart.quentity;
    li_products.append(span_price);

    var li_shopping = document.createElement("li");
    li_shopping.className = "list-group-item d-flex justify-content-between align-items-center px-0";
    li_shopping.innerText = "Shopping";

    var span_gratis = document.createElement("span");
    span_gratis.innerText = "Gratis";
    li_shopping.append(span_gratis);

    var li_price = document.createElement("li");
    li_price.className = "list-group-item d-flex justify-content-between align-items-center border-0 px-0 mb-3";
    
    var div_price = document.createElement("div");

    var text_1 = document.createElement("strong");
    text_1.innerText = "Total amount";

    var text_2 = document.createElement("strong");

    var p = document.createElement("p");
    p.className = "mb-0";
    p.innerText = "(including VAT)";
    text_2.append(p);

    div_price.append(text_1);
    div_price.append(text_2);

    var span_price_end = document.createElement("span");
    var text_3 = document.createElement("strong");
    text_3.setAttribute("id", "price");
    text_3.innerText = "$" + cart.price.toFixed(2);

    span_price_end.append(text_3);

    li_price.append(div_price);
    li_price.append(span_price_end);

    list_group.append(li_products);
    list_group.append(li_shopping);
    list_group.append(li_price);

    var btn = document.createElement("button");
    btn.setAttribute("type", "button");
    btn.className = "btn btn-primary btn-lg btn-block";
    btn.innerText = "Go to checkout";
    btn.addEventListener("click", redirect);

    card_body.append(list_group);
    card_body.append(btn);

    card.append(card_header);
    card.append(card_body);

    summary[0].append(card);
}

function redirect() {
    window.location.href = "https://localhost:7270/Home/Index";
}

function cartBox(cart) {
    var cart_list = document.getElementsByClassName("cart-list");

    var card = document.createElement("div");
    card.className = "card cart-box overflow-auto mb-4";

    var card_header = document.createElement("div");
    card_header.className = "card-header py-3";

    var h5 = document.createElement("h5");
    h5.className = "mb-0";
    h5.innerText = "Cart - " + cart.cartDetails.length + " items";

    card_header.append(h5);
    card.append(card_header);

    var card_body = document.createElement("div");
    card_body.className = "card-body";
    cart.cartDetails.forEach(cd => card_body.append(cartRows(cd)));

    card.append(card_body);
    addSummary(cart);

    cart_list[0].append(card);
}

function cartRows(cd) {
    var row = document.createElement("div");
    row.classList = "row cart-rows mx-2 mb-3";
    row.setAttribute("id", "cartDetailsId-" + cd.cartDetailsId);

    var img_col = document.createElement("div");
    img_col.className = "col-lg-3 col-md-12 mb-4 mb-lg-0";

    var img_div = document.createElement("div");
    img_div.className = "bg-image hover-overlay hover-zoom ripple rounded";

    var img = document.createElement("img");
    img.src = cd.item.photoUrl;
    img.className = "w-100";

    img_div.append(img);
    img_col.append(img_div);
    row.append(img_col);


    var info_col = document.createElement("div");
    info_col.className = "col-lg-4 col-md-6 mb-4 mb-lg-0";

    var name_p = document.createElement("p");

    var name_strong = document.createElement("strong");
    name_strong.innerText = cd.item.name;
    name_p.append(name_strong);

    var category_p = document.createElement("p");
    category_p.innerText = "Category: " + cd.item.categoryName;

    var btn_trash = document.createElement("button");
    btn_trash.className = "btn btn-primary btn-sm me-1 mb-2";
    btn_trash.addEventListener("click", async () => await removeFromCart(cd.cartDetailsId));

    var i_trash = document.createElement("i");
    i_trash.className = "fas fa-trash";
    btn_trash.append(i_trash);

    var btn_heart = document.createElement("button");
    btn_heart.className = "btn btn-danger btn-sm mb-2";

    var i_heart = document.createElement("i");
    i_heart.className = "fas fa-heart";
    btn_heart.append(i_heart);

    info_col.append(name_p);
    info_col.append(category_p);
    info_col.append(btn_trash);
    info_col.append(btn_heart);

    row.append(info_col);


    var input_col = document.createElement("div");
    input_col.className = "col-lg-5 col-md-6 mb-4 mb-lg-0";

    var input_flex = document.createElement("div");
    input_flex.className = "d-flex justify-content-lg-center mb-4";

    var btn_minus = document.createElement("button");
    btn_minus.className = "btn btn-primary me-2";
    btn_minus.addEventListener("click", async () => await countLower(cd));

    var i_minus = document.createElement("i");
    i_minus.className = "fas fa-minus";
    btn_minus.append(i_minus);

    var input_form = document.createElement("div");
    input_form.className = "form-outline";

    var quentity = document.createElement("div");
    quentity.className = "m-2 cart-input";
    quentity.setAttribute("id", "quentity-" + cd.cartDetailsId)
    quentity.innerText = cd.count;
    input_form.append(quentity);

    var btn_plus = document.createElement("button");
    btn_plus.className = "btn btn-primary ms-2";
    btn_plus.addEventListener("click", async () => await countUpper(cd));

    var i_plus = document.createElement("i");
    i_plus.className = "fas fa-plus";
    btn_plus.append(i_plus);

    input_flex.append(btn_minus);
    input_flex.append(input_form);
    input_flex.append(btn_plus);


    var price_p = document.createElement("p");
    price_p.className = "d-flex justify-content-lg-center";

    var price_strong = document.createElement("strong");
    price_strong.setAttribute("id", "sum-price-" + cd.cartDetailsId);
    price_strong.innerText = "$" + (cd.item.price * cd.count).toFixed(2);
    price_p.append(price_strong);

    input_col.append(input_flex);
    input_col.append(price_p);

    row.append(input_col);

    return row;
}

function changePriceAndQuentity(details_quentity, details_price, all_quentity, all_price, cd) {
    if (details_quentity != 0) {
        var cd_quentity = document.getElementById("quentity-" + cd.cartDetailsId);
        cd_quentity.innerText = parseInt(cd_quentity.innerText) + details_quentity;
    }

    if (details_price != 0) {
        var cd_price = document.getElementById("sum-price-" + cd.cartDetailsId);
        cd_price.innerText = "$" + (parseFloat(cd_price.innerText.replace('$', '')) + details_price * cd.item.price).toFixed(2);
    }

    if (all_quentity != 0) {
        var vall_quentity = document.getElementById("quentity-products");
        vall_quentity.innerText = parseInt(vall_quentity.innerText) + all_quentity;
    }

    if (all_price != 0) {
        var vall_price = document.getElementById("price");
        vall_price.innerText = "$" + (parseFloat(vall_price.innerText.replace('$', '')) + all_price * cd.item.price).toFixed(2);
    }

}

function clearCartPage() {
    document.getElementsByClassName("cart-list")[0].innerHTML = "";
    document.getElementsByClassName("cart-summary")[0].innerHTML = "";
}