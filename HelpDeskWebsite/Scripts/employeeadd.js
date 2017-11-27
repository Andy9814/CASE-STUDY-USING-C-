$(function () { //employeeadd.js

    getAll(''); //first grab the data from the server


    //click event handler for the student list
    $('#employeeList').click(function (e) {  //click on any row
        if (!e) e = window.event;
        var Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
            Id = e.target.id; //clicked on row somehwere else

        }
        var data = JSON.parse(localStorage.getItem('allemployees'));
        clearModalFields();

        //now figure out if we're doing an add or update/delete?
        if (Id === '0') {
            setupForAdd();

        } else {
            setupForUpdate(Id, data);

        }


    });

    $('#actionbutton').click(function () {
        if ($('#actionbutton').val() === 'Update') {
            update();
        }
        else {
            add();

        }
        return false;
    }); //actionbutton click

    function setupForUpdate(Id, data) {
        $('#actionbutton').val('Update');
        $('#modaltitle').html('<h4>Employee Update</h4>');
        $('#deletebutton').show();

        $.each(data, function (index, employee) {
            if (employee.Id === parseInt(Id)) {
                $('#TextBoxTitle').val(employee.Title);
                $('#TextBoxFirstname').val(employee.Firstname);
                $('#TextBoxLastname').val(employee.Lastname);
                $('#TextBoxPhone').val(employee.Phoneno);
                $('#TextBoxEmail').val(employee.Email);
                localStorage.setItem('Id', employee.Id);
                localStorage.setItem('DepartmentId', employee.DepartmentId);
                localStorage.setItem('Timer', employee.Timer);
                return false; //breaks out of loop

            } else {
                $('#status').text('no employee found');
            }
        });
        $('#theModal').modal('toggle');
    }


    function setupForAdd() {
        clearModalFields();
        $('#actionbutton').val('Add');
        $('#modaltitle').html('<h4>Add New Employee</h4>');
        $('#adeletebutton').hide();
        $('#theModal').modal('toggle');
    }

    //build the list
    function buildEmployeeList(data) {
        $('#employeeList').empty();
        div = $('<div class="list-group"><div>' +
            '<span class="col-xs-4 h3">Title</span>' +
            '<span class="col-xs-4 h3">First</span>' +
            '<span class="col-xs-4 h3">Last</span>' +
            '</div>');
        div.appendTo($('#employeeList'))
        localStorage.setItem('allemployees', JSON.stringify(data));
        btn = $('<button class="list-group-item" id="0">...Click to add employee</button>');
        btn.appendTo(div);

        $.each(data, function (index, emp) {
            btn = $('<button class="list-group-item" id="' + emp.Id + '">');
            btn.html(
                '<span class="col-xs-4" id=employeetitle' + emp.Id + '">' + emp.Title + '</span>' +
                '<span class="col-xs-4" id=employeefname' + emp.Id + '">' + emp.Firstname + '</span>' +
                '<span class="col-xs-4" id=employeelastname' + emp.Id + '">' + emp.Lastname + '</span>'
            );
            btn.appendTo(div);


        }); //each
    } //buildEmployeeList



    //get all employees
    function getAll(msg) {
        $('#status').text('Finding Employee Information...');


        ajaxCall('Get', 'api/employees', '')
            .done(function (data) {
                buildEmployeeList(data);
                if (msg === '')
                    $('#status').text('Employees loaded');
                else
                    $('#status').text(msg + ' - Employees Loaded');

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);

            });

    } //getAll


    function clearModalFields() {
        $('#TextBoxTitle').val('');
        $('#TextBoxFirstname').val('');
        $('#TextBoxLastname').val('');
        $('#TextBoxPhone').val('');
        $('#TextBoxEmail').val('');
        $('#TextBoxStatus').val('');
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');

    }

    function add() {
        emp = new Object();
        emp.Title = $('#TextBoxTitle').val();
        emp.Firstname = $('#TextBoxFirstname').val();
        emp.Lastname = $('#TextBoxLastname').val();
        emp.Phoneno = $('#TextBoxPhone').val();
        emp.Email = $('#TextBoxEmail').val();
        emp.DepartmentId = 100; //we'll hardcode now, add a drop down later


        ajaxCall('Post', 'api/employees', emp)
            .done(function (data) {
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });

        $('#theModal').modal('toggle');
        return false; //make sure to return false for click or REST calls get cancelled

    } //add



    //update button click event handler

    function update() {
        emp = new Object();
        emp.Title = $('#TextBoxTitle').val();
        emp.Firstname = $('#TextBoxFirstname').val();
        emp.Lastname = $('#TextBoxLastname').val();
        emp.Phoneno = $('#TextBoxPhone').val();
        emp.Email = $('#TextBoxEmail').val();
        emp.Id = localStorage.getItem('Id');
        emp.DepartmentId = localStorage.getItem('DepartmentId');
        emp.Timer = localStorage.getItem('Timer');


        ajaxCall('put', 'api/employees', emp)
            .done(function (data) {
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });

        $('#theModal').modal('toggle');
        return false; //make sure to return false for click or REST calls get cancelled



    }//update


    //call ASP.Net WEB API server
    function ajaxCall(type, url, data) {
        return $.ajax({ // return the promise that '$.ajax'  returns
            type: type,
            url: url,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: true

        });
    } //ajaxCall

    function errorRoutine(jqXHR) { //common error
        if (jqXHR.responseJSON === null) {
            $('#status').text(jqXHR.responseText);
        }
        else {
            $('#status').text(jqXHR.responseText);
        }

    } //errorRoutine
});
