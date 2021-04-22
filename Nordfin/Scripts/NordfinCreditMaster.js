

function save() {
    var dt = new Date();

    dt.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById("spnCalendar").innerHTML = dt.toLocaleDateString('en-US', { weekday: 'long' });
    document.getElementById("spnDate").innerHTML = dt.toString().split(' ')[1] + " " + dt.toString().split(' ')[2];
}
$(document).ready(function () {

    $('.topMenuOpenDropdown').click(function () {
        $('.dropdownMenu').toggleClass('hidden');
        $('.topMenuOpenDropdown').toggleClass('dropdownOpen');
        $('.topMenuSearchContainer').toggleClass('dropdownOpen');

    });
});

function logout() {
    document.getElementById("btnlogout").click();
}