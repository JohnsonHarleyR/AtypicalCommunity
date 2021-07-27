// show items on avatar that are already in the model
function showAvatar() {
    // grab all the divs for the avatar display
    let elements = document.querySelectorAll('.avatar-img-div');
    // loop through the elements
    for (let i = 0; i < elements.length; i++) {
        let item = getAvatarProperty(elements[i].id);
        let itemId;
        if (item === null) {
            itemId = -1;
        } else {
            itemId = item.Id;
        }

        // show item on avatar
        showItemOnAvatar(itemId, elements[i].id);

    }

}

// show item on avatar
function showItemOnAvatar(itemId, elementId) {
    console.log("showing item");
    let avatarItem;
    if (itemId == -1) { // -1 indicates that no item should be displayed
        avatarItem = null;
    } else {
        avatarItem = getItemById(itemId);
    }
    let avatarLayer = document.getElementById(elementId);
    if (avatarItem === null) {
        avatarLayer.innerHTML = "";
    } else {
        avatarLayer.innerHTML = '<img class="avatar-img" src="' +
            avatarItem.Url + '">';
    }
    // also update the avatar property with this item
    updateAvatarProperty(avatarItem, elementId);
}

function getAvatarProperty(elementId) {
    switch (elementId) {
        case "backgroundLayer":
            return avatar.Background;
        case "secondaryBackgroundLayer":
            return avatar.SecondaryBackground;
        case "foregroundLayer":
            return avatar.Foreground;
        case "baseLayer":
            return avatar.Base;
        case "tattoosLayer":
            return avatar.Tattoos;
        case "marksLayer":
            return avatar.Marks;
        case "eyesLayer":
            return avatar.Eyes;
        case "noseLayer":
            return avatar.Nose;
        case "mouthLayer":
            return avatar.Mouth;
        case "makeupLayer":
            return avatar.Makeup;
        case "facialHairLayer":
            return avatar.FacialHair;
        case "earRingsLayer":
            return avatar.EarRings;
        case "facePiercingsLayer":
            return avatar.FacePiercings;
        case "necklaceLayer":
            return avatar.Necklace;
        case "leftArmLayer":
            return avatar.LeftArm;
        case "rightArmLayer":
            return avatar.RightArm;
        case "hairLayer":
            return avatar.Hair;
        case "hairAccessoryLayer":
            return avatar.HairAccessory;
        case "hatLayer":
            return avatar.Hat;
        case "topLayer":
            return avatar.Top;
        case "fullBodyLayer":
            return avatar.FullBody;
        case "neckLayer":
            return avatar.Neck;
        case "bottomLayer":
            return avatar.Bottom;
        case "shoesLayer":
            return avatar.Shoes;
        case "leftAccessoryLayer":
            return avatar.LeftAccessory;
        case "rightAccessoryLayer":
            return avatar.RightAccessory;
        case "leftHandLayer":
            return avatar.LeftHand;
        case "rightHandLayer":
            return avatar.RightHand;

    }
}

function updateAvatarProperty(item, elementId) {
    switch (elementId) {
        case "backgroundLayer":
            avatar.Background = item;
            break;
        case "secondaryBackgroundLayer":
            avatar.SecondaryBackground = item;
            break;
        case "foregroundLayer":
            avatar.Foreground = item;
            break;
        case "baseLayer":
            avatar.Base = item;
            break;
        case "tattoosLayer":
            avatar.Tattoos = item;
            break;
        case "marksLayer":
            avatar.Marks = item;
            break;
        case "eyesLayer":
            avatar.Eyes = item;
            break;
        case "noseLayer":
            avatar.Nose = item;
            break;
        case "mouthLayer":
            avatar.Mouth = item;
            break;
        case "makeupLayer":
            avatar.Makeup = item;
            break;
        case "facialHairLayer":
            avatar.FacialHair = item;
            break;
        case "earRingsLayer":
            avatar.EarRings = item;
            break;
        case "facePiercingsLayer":
            avatar.FacePiercings = item;
            break;
        case "necklaceLayer":
            avatar.Necklace = item;
            break;
        case "leftArmLayer":
            avatar.LeftArm = item;
            break;
        case "rightArmLayer":
            avatar.RightArm = item;
            break;
        case "hairLayer":
            avatar.Hair = item;
            break;
        case "hairAccessoryLayer":
            avatar.HairAccessory = item;
            break;
        case "hatLayer":
            avatar.Hat = item;
            break;
        case "topLayer":
            avatar.Top = item;
            break;
        case "fullBodyLayer":
            avatar.FullBody = item;
            break;
        case "neckLayer":
            avatar.Neck = item;
            break;
        case "bottomLayer":
            avatar.Bottom = item;
            break;
        case "shoesLayer":
            avatar.Shoes = item;
            break;
        case "leftAccessoryLayer":
            avatar.LeftAccessory = item;
            break;
        case "rightAccessoryLayer":
            avatar.RightAccessory = item;
            break;
        case "leftHandLayer":
            avatar.LeftHand = item;
            break;
        case "rightHandLayer":
            avatar.RightHand = item;
            break;
        
    }

}

// get item by its id
function getItemById(itemId) {
    for (let i = 0; i < avatarItems.length; i++) {
        if (avatarItems[i].Id === itemId) {
            return avatarItems[i];
        }
    }
}


// putting related items in list

// get all avatar items with a particular sub category
function showAvatarItems(elementId, subCategory) {
    let element = document.getElementById(elementId);
    let items = getAvatarItems(subCategory);

    console.log(items);

    // show items and set properties
    element.innerHTML = "";
    for (let i = 0; i < items.length; i++) {
        let subCategoryId = subCategory.charAt(0).toLowerCase() + subCategory.slice(1) + "Layer";
        // if i is 0 and the subcategory can have a blank, add a blank square too
        if (i === 0 && subCategory != 'Base' && subCategory != 'Eyes' &&
            subCategory != 'Nose' && subCategory != "mouth") {
            element.innerHTML += '<img class="item-icon" id="item' + items[i].Id +
                '" onclick="showItemOnAvatar(-1, \'' + subCategoryId + '\')"'
                + ' src="/Images/Avatar/Items/Empty/empty-icon.png">';
        }
        element.innerHTML += '<img class="item-icon" id="item' + items[i].Id +
            '" onclick="showItemOnAvatar(' + items[i].Id + ', \'' + subCategoryId + '\')"'
            + ' src="' + items[i].IconUrl + '">';
    }

}

// returns all avatar items under a certain sub category
function getAvatarItems(subCategory) {
    let items = new Array();
    for (let i = 0; i < avatarItems.length; i++) {
        if (avatarItems[i].SubCategory === subCategory) {
            items.push(avatarItems[i]);
        }
    }
    return items;
}




// Tab functions

function openCategory(evt, category) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(category).style.display = "block";
    evt.currentTarget.className += " active";
}

function openSubCategory(evt, subCategory) {
    // Declare all variables
    var i, tabcontent, tablinks;

    // Get all elements with class="tabcontent" and hide them
    tabcontent = document.getElementsByClassName("tabcontent1");
    for (i = 0; i < tabcontent.length; i++) {
        tabcontent[i].style.display = "none";
    }

    // Get all elements with class="tablinks" and remove the class "active"
    tablinks = document.getElementsByClassName("tablinks1");
    for (i = 0; i < tablinks.length; i++) {
        tablinks[i].className = tablinks[i].className.replace(" active", "");
    }

    // Show the current tab, and add an "active" class to the button that opened the tab
    document.getElementById(subCategory).style.display = "block";
    evt.currentTarget.className += " active";

    // show display info for the subcategory
    showAvatarItems(subCategory, subCategory);

}


// event handlers
document.body.onload = showAvatar();