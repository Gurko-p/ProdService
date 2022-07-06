let products = null;
let processings = null;
let meals = null;
let dishCarts = null;

async function submitForm(e) {
    e.preventDefault();
    let submitBtn = e.target;
    let dataProp = submitBtn.getAttribute("data-prop");
    let formId = `#add${dataProp}form`;
    let messageContainer = document.querySelector(`${formId} > .message-container`);

    let pdcjIds = document.querySelectorAll("#pdcjId");
    let dishCartId = document.querySelector("#adddishCartsform #dishCartId").value;
    let dishName = document.querySelector("#dishName").value;
    let products = document.querySelectorAll(".products");
    let processings = document.querySelectorAll(".processings");
    let weightBruttos = document.querySelectorAll(".weightBrutto");
    let dishAddedId = dishCartId;
    if (dishCartId == 0) {
        dishAddedId = await fetch('/api/dishcarts', {
            method: 'POST', headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: dishCartId, name: `${dishName}` })
        }).then(r => r.json());
    }
    for (let i = 0; i < products.length; i++) {
        let response = await fetch('/api/productDishCarts', {
            method: 'POST', headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: `${pdcjIds[i].value}`,
                ProductId: `${products[i].value}`,
                DishCartId: `${dishAddedId}`,
                ProcessingId: `${processings[i].value}`,
                WeightBrutto: `${weightBruttos[i].value}`
            })
        });
        let infoRow = createQueryResultMessage(response);
        if (!checkChildNodes(`${formId} > .message-container`)) {
            messageContainer.appendChild(infoRow);
            if (submitBtn.classList.contains("edit-submit-btn")) {
                submitBtn.setAttribute("disabled", "disabled");
                setTimeout(() => {
                    submitBtn.classList.remove("edit-submit-btn");
                    submitBtn.removeAttribute("disabled");
                    messageContainer.removeChild(messageContainer.firstChild);
                    document.querySelector(`#add${dataProp} .btn-close`).click();
                    getList(e, `${dataProp}`, `${submitBtn.getAttribute("data-listName")}`);
                    clearFormDishCart(formId);
                }, 1500);

            } else {
                setTimeout(() => {
                    messageContainer.removeChild(messageContainer.firstChild);
                    clearFormDishCart(formId);
                    getProdProcessings(e);
                }, 3000);

            }
        }

    }


}

function createRow(selectProcessingTypes, selectProducts, valueWeight, pdijId) {
    let div = document.createElement("div");

    let inputId = document.createElement("input");
    inputId.setAttribute("type", "hidden");
    inputId.setAttribute("value", `${pdijId}`);
    inputId.setAttribute("id", "pdcjId");
    inputId.setAttribute("name", "id");

    let selectProds = selectProducts;
    let selectProcTypes = selectProcessingTypes;

    let divForProducts = document.createElement("div");
    divForProducts.setAttribute("class", "divForProducts");
    let divForProcessings = document.createElement("div");
    divForProcessings.setAttribute("class", "divForProcessings");
    let divForInputWeigth = document.createElement("div");
    divForInputWeigth.setAttribute("class", "divForInputWeigth");


    let inputWeigth = document.createElement("input");

    let inputDel = document.createElement("button");
    inputWeigth.setAttribute("type", "number");
    inputWeigth.setAttribute("class", "weightBrutto form-control");
    inputWeigth.setAttribute("placeholder", "Вес, г");
    inputWeigth.setAttribute("step", "0.1");
    inputWeigth.setAttribute("min", "0.0");
    inputWeigth.setAttribute("value", `${valueWeight}`);
    inputDel.setAttribute("type", "button");
    inputDel.innerText = "Удалить";
    inputDel.setAttribute("class", "btn btn-primary");
    inputDel.setAttribute("id", `${pdijId}`);
    inputDel.addEventListener("click", (e) => deleteDishCart(e));


    divForProducts.appendChild(selectProds);
    divForProcessings.appendChild(selectProcTypes);
    divForInputWeigth.appendChild(inputWeigth)
    div.appendChild(inputId);
    div.appendChild(divForProducts);
    div.appendChild(divForProcessings);
    div.appendChild(divForInputWeigth);
    div.appendChild(inputDel);
    return div;
}

function fillOptions(data, className, optionValue) {
    let select = document.createElement("select");
    select.setAttribute("class", `${className}`);
    for (let i = 0; i < data.length; i++) {
        let option = document.createElement("option");
        option.setAttribute("value", `${data[i].id}`);
        option.innerText = `${data[i].name}`;
        if (data[i].id == optionValue) {
            option.setAttribute("selected", "selected");
        }
        select.appendChild(option);
    }
    return select;
}

async function deleteDishCart(e) {

    let id = e.target.getAttribute("id");
    if (id != 0) {
        let response = await fetch(
            `/api/productDishCarts/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (response.ok) {
            e.target.parentNode.remove();
        }
        else {
            alert("Что - то пошло не так...Обратитесь к администратору!");
        }
    }
    else {
        e.target.parentNode.remove();
    }
}

function clearFormDishCart(formId) {
    let dishAddContainer = document.querySelectorAll("input");
    for (let i = 0; i < dishAddContainer.length; i++) {
        dishAddContainer[i].getAttribute("name") == "id" ? dishAddContainer[i].value = 0 : dishAddContainer[i].value = "";
    }
    let dishContainsProd = document.querySelector(`${formId} #dishContainsProd`);
    let dishContainsProdDivs = document.querySelectorAll(`${formId} #dishContainsProd > div`);

    for (let i = 0; i < dishContainsProdDivs.length; i++) {
        dishContainsProd.removeChild(dishContainsProd.firstChild);
    }
}

async function getProdProcessings(e, selectedOptionProduct = 0, selectedOptionProcessing = 0, valueWeight = 0, pdijId = 0) {

    if (pdijId == 0 && e.target.classList.contains("editFromListBtn") || e.target.classList.contains("add-new-value")) {
        clearFormDishCart(`#add${e.target.getAttribute("name")}form`);
    }
    let dishContainsProd = document.querySelector("#dishContainsProd");
    let selectProducts = null;
    let selectProcessingTypes = null;
    if (products == null || processings == null) {
        products = await fetch('/Home/GetProducts').then(r => r.json());
        processings = await fetch('/Home/GetProcessings').then(r => r.json());
        selectProducts = fillOptions(products, "products form-control", selectedOptionProduct);
        selectProcessingTypes = fillOptions(processings, "processings form-control", selectedOptionProcessing);
    }
    else {
        selectProducts = fillOptions(products, "products form-control", selectedOptionProduct);
        selectProcessingTypes = fillOptions(processings, "processings form-control", selectedOptionProcessing);
    }

    let row = await createRow(selectProcessingTypes, selectProducts, valueWeight, pdijId);
    dishContainsProd.appendChild(row);
}

async function editDishCart(e) {
    let id = e.target.getAttribute("id");
    let targetName = e.target.getAttribute("name");
    let formId = `#add${targetName}form`;
    clearFormDishCart(formId);
    let submitBtn = document.querySelector(`${formId} #submitBtn`);
    submitBtn.classList.add("edit-submit-btn");
    let dishCart = await fetch(
        `/api/${targetName}/${id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(r => r.json());
    let dishAddContainer = document.querySelectorAll(`${formId} .dishAddContainer input`); //<div>

    for (let i = 0; i < dishAddContainer.length; i++) {
        dishAddContainer[i].value = dishCart[`${dishAddContainer[i].getAttribute("name")}`];
    }
    let technologicCarts = await fetch(
        `/Home/GetTechnologicCarts?dishCartId=${dishCart.id}`, {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    }).then(r => r.json());
    for (let i = 0; i < technologicCarts.length; i++) {
        getProdProcessings(e, technologicCarts[i].productId, technologicCarts[i].processingId, technologicCarts[i].weightBrutto, technologicCarts[i].id)
    }
}




//MenuItem
async function getMenuItems(e, selectedOptionMeal = 0, selectedOptiondishCart = 0, midcjId = 0) {
    let menuItemContainsDishCart = document.querySelector("#menuItemContainsDishCart");
    if (e.target.classList.contains("add-new-value")) {
        clearFormMenuItem(`#add${e.target.getAttribute("name")}form`);
    }
    let selectMeals = null;
    let selectdishCarts = null;
    if (meals == null || dishCarts == null) {
        meals = await fetch('/api/meals').then(r => r.json());
        dishCarts = await fetch('/api/dishCarts').then(r => r.json());
        selectMeals = fillOptions(meals, "meals form-control", selectedOptionMeal);
        selectdishCarts = fillOptions(dishCarts, "dishCarts form-control", selectedOptiondishCart);
    }
    else {
        selectMeals = fillOptions(meals, "meals form-control", selectedOptionMeal);
        selectdishCarts = fillOptions(dishCarts, "dishCarts form-control", selectedOptiondishCart);
    }
    let row = createRowMenuItem(selectMeals, selectdishCarts, midcjId);
    menuItemContainsDishCart.appendChild(row);
}

async function editMenuItems(e) {
    e.preventDefault();
    e.stopPropagation();
    clearFormMenuItem(`#add${e.target.getAttribute("name")}form`);
    let submitBtnMenuItem = document.querySelector("#submitBtnMenuItem");
    submitBtnMenuItem.classList.add("edit-submit-btn");
    let inputs = document.querySelectorAll(".menuItemAddContainer > input");
    let menuItemContainsDishCart = document.querySelector("#menuItemContainsDishCart");
    let menuItemId = e.target.getAttribute("id");
    let menuItemController = e.target.getAttribute("name");
    let selectMeals = null;
    let selectdishCarts = null;
    let menuItem = await fetch(`/api/${menuItemController}/${menuItemId}`).then(r => r.json());
    inputs[0].value = menuItem.id;
    inputs[1].value = menuItem.date.slice(0, 10);
    let menuItemDishCartJunctions = await fetch(`/home/GetDishCartMenuItems?menuItemId=${menuItemId}`).then(r => r.json())
    meals = await fetch('/api/meals').then(r => r.json());
    dishCarts = await fetch('/api/dishCarts').then(r => r.json());
    for (let i = 0; i < menuItemDishCartJunctions.length; i++) {
        selectMeals = await fillOptions(meals, "meals form-control", `${menuItemDishCartJunctions[i].mealId}`);
        selectdishCarts = await fillOptions(dishCarts, "dishCarts form-control", `${menuItemDishCartJunctions[i].dishCartId}`);
        let row = createRowMenuItem(selectMeals, selectdishCarts, `${menuItemDishCartJunctions[i].id}`);
        menuItemContainsDishCart.appendChild(row);
    }
}

function createRowMenuItem(selectMeals, selectdishCarts, midcjId) {
    let div = document.createElement("div");

    let inputId = document.createElement("input");
    inputId.setAttribute("type", "hidden");
    inputId.setAttribute("value", `${midcjId}`);
    inputId.setAttribute("id", "midcjId");
    inputId.setAttribute("name", "id");

    let selMeals = selectMeals;
    let seldishCarts = selectdishCarts;

    let divForMeals = document.createElement("div");
    divForMeals.setAttribute("class", "divForMeals");
    let divFordishCarts = document.createElement("div");
    divFordishCarts.setAttribute("class", "divFordishCarts");

    let inputDel = document.createElement("button");
    inputDel.setAttribute("type", "button");
    inputDel.innerText = "Удалить";
    inputDel.setAttribute("class", "btn btn-primary");
    inputDel.setAttribute("id", `${midcjId}`);
    inputDel.addEventListener("click", (e) => deleteDishCartMenuItemJunction(e));


    divForMeals.appendChild(selMeals);
    divFordishCarts.appendChild(seldishCarts);
    div.appendChild(inputId);
    div.appendChild(divForMeals);
    div.appendChild(divFordishCarts);
    div.appendChild(inputDel);
    return div;
}

async function deleteDishCartMenuItemJunction(e) {

    let id = e.target.getAttribute("id");
    if (id != 0) {
        let response = await fetch(
            `/api/dishCartMenuItemJunctions/${id}`, {
            method: 'DELETE',
            headers: {
                'Content-Type': 'application/json'
            }
        });
        if (response.ok) {
            e.target.parentNode.remove();
        }
        else {
            alert("Что - то пошло не так...Обратитесь к администратору!");
        }
    }
    else {
        e.target.parentNode.remove();
    }
}

function clearFormMenuItem(formId) {
    let menuItemAddContainer = document.querySelectorAll("input");
    for (let i = 0; i < menuItemAddContainer.length; i++) {
        menuItemAddContainer[i].getAttribute("name") == "id" ? menuItemAddContainer[i].value = 0 : menuItemAddContainer[i].value = "";
    }
    let menuItemContainsDishCart = document.querySelector(`${formId} #menuItemContainsDishCart`);
    let menuItemContainsDishCartDivs = document.querySelectorAll(`${formId} #menuItemContainsDishCart > div`);

    for (let i = 0; i < menuItemContainsDishCartDivs.length; i++) {
        menuItemContainsDishCart.removeChild(menuItemContainsDishCart.firstChild);
    }
}

async function submitFormMenuItem(e) {
    e.preventDefault();
    let submitBtn = e.target;
    let dataProp = submitBtn.getAttribute("data-prop"); //menuItems
    let formId = `#add${dataProp}form`; // #addmenuItemsform
    let messageContainer = document.querySelector(`${formId} > .message-container`);

    let midcjIds = document.querySelectorAll("#midcjId");

    let menuItemId = document.querySelector(`${formId} #menuItemId`).value;
    let menuItemDate = document.querySelector(`${formId} #menuItemDate`).value;
    let meals = document.querySelectorAll(`${formId} .meals`);

    let dishCarts = document.querySelectorAll(`${formId} .dishCarts`);
    let menuItemAddedId = menuItemId;
    if (menuItemId == 0) {
        menuItemAddedId = await fetch(`/api/${dataProp}`, {
            method: 'POST', headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({ id: menuItemId, date: `${menuItemDate}` })
        }).then(r => r.json());
    }
    for (let i = 0; i < meals.length; i++) {
        let response = await fetch('/api/dishCartMenuItemJunctions', {
            method: 'POST', headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                Id: `${midcjIds[i].value}`,
                DishCartId: `${dishCarts[i].value}`,
                MenuItemId: `${menuItemAddedId}`,
                MealId: `${meals[i].value}`
            })
        });
        let infoRow = createQueryResultMessage(response);
        if (!checkChildNodes(`${formId} > .message-container`)) {
            messageContainer.appendChild(infoRow);
            if (submitBtn.classList.contains("edit-submit-btn")) {
                submitBtn.setAttribute("disabled", "disabled");
                setTimeout(() => {
                    submitBtn.classList.remove("edit-submit-btn");
                    submitBtn.removeAttribute("disabled");
                    messageContainer.removeChild(messageContainer.firstChild);
                    document.querySelector(`#add${dataProp} .btn-close`).click();
                    clearFormMenuItem(formId);
                    let inputsDates = document.querySelectorAll(`#MenuDates > input`);
                    inputsDates[0].value = menuItemDate;
                    inputsDates[1].value = menuItemDate;
                    document.querySelector("#showMenuBtn").click();
                }, 1500);

            } else {
                setTimeout(() => {
                    messageContainer.removeChild(messageContainer.firstChild);
                    clearFormMenuItem(formId);
                    getMenuItems(e);
                }, 3000);
            }
        }

    }
}

function showMenuOnDate() {
    let menuItemDate = document.querySelector("#menuItemDate").value;
    let inputsDates = document.querySelectorAll(`#MenuDates > input`);
    inputsDates[0].value = menuItemDate;
    inputsDates[1].value = menuItemDate;
    document.querySelector("#showMenuBtn").click();
}


let btnAddDishToMenu = document.querySelector("#addDishToMenu");
btnAddDishToMenu.addEventListener("click", (e) => getMenuItems(e));

let btn = document.querySelector("#addProductSelect");

btn.addEventListener("click", (e) => getProdProcessings(e));

let submitBtn = document.querySelector("#submitBtn");
submitBtn.addEventListener("click", submitForm);

let submitBtnMenuItem = document.querySelector("#submitBtnMenuItem");
submitBtnMenuItem.addEventListener("click", (e) => submitFormMenuItem(e));

let showMenuOnDateBtn = document.querySelector(".showMenu");
showMenuOnDateBtn.addEventListener("click", () => showMenuOnDate());

