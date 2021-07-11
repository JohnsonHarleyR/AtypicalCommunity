// When the page loads, the user info should be stored as a variable to access their info
// The only way anything should be accessible is if the Session has a user stored.


// grab all the necessary info and show it in the layout
function grabNecessaryInfo() {

    // grab all the needed info and show necessary info in the layout
    grabUserInfo();
    grabCoinInfo();

}

// grab the user information and store it as a variable
function grabUserInfo() {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', '/Ajax/GetUserInfo', true);
    xhttp.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhttp.onreadystatechange = function () {

        if (this.readyState == 4 && this.status == 200) {
            // response
            user = JSON.parse(this.response);
            console.log(user);
            showUserTypeInfo();
        }

    };

    xhttp.send();
}

// grab the user information and store it as a variable
function grabCoinInfo() {
    var xhttp = new XMLHttpRequest();
    xhttp.open('POST', '/Ajax/GetCoinInfo', true);
    xhttp.setRequestHeader('Content-type', 'application/json; charset=utf-8');
    xhttp.onreadystatechange = function () {

        if (this.readyState == 4 && this.status == 200) {
            // response
            bankAccount = JSON.Parse(this.response);
            console.log(bankAcount);
            showCoinInfo();
        }

    };

    xhttp.send();
}

function showCoinInfo() {
    // show coin information
    coinDisplay.innerHTML = bankAccount.Checking;
}

function showUserTypeInfo() {
    if (user.UserType != 'Citizen') {
        userType.innerHTML = user.UserType; // see how this shows up
    }

}


// variables
var coinDisplay = document.getElementById('coinAmount');
var userType = document.getElementById('userType');

var user;
var bankAccount;

// event handlers
document.body.onload = grabNecessaryInfo();