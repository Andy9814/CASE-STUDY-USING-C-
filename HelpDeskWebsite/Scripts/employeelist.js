$(function () { 

    getAll(''); 

    //click event handler
    $('#employeeList').click(function (e) { 
        if (!e) e = window.event;
        var Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
            //clicked on row 
            Id = e.target.id; 
        }
        var data = JSON.parse(localStorage.getItem('allemployees'));
        clearModalFields();

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
                return false; 
            } else {
                $('#status').text("no employee found");
            }
        });

        $("#theModal").modal("toggle");
    });

    //update button click event handler
    $('#empupdbutton').click(function () {
        emp = new Object();
        emp.Title = $("#TextBoxTitle").val();
        emp.Firstname = $("#TextBoxFirstname").val();
        emp.Lastname = $("#TextBoxLastname").val();
        emp.Phoneno = $("#TextBoxPhone").val();
        emp.Email = $("#TextBoxEmail").val();
        emp.Id = localStorage.getItem("Id");
        emp.DepartmentId = localStorage.getItem("DepartmentId");
        emp.Timer = localStorage.getItem('Timer');

        ajaxCall('put', 'api/employees', emp)
            .done(function (data) {
                $("#theModal").modal("toggle");
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        return false; 
    });

    //build the list
    function buildEmployeeList(data) {
        $('#employeeList').empty();
        div = $('<div class="list-group"><div>' +
            '<span class="col-xs-4 h3">Title </span>' +
            '<span class="col-xs-4 h3"> First </span>' +
            '<span class="col-xs-4 h3"> Last </span>' +
            '</div>');
        div.appendTo($('#employeeList'))
        localStorage.setItem('allemployees', JSON.stringify(data));

        $.each(data, function (index, emp) {
            btn = $('<button class="list-group-item" id="' + emp.Id + '">');
            btn.html(
                '<span class="col-xs-4" id=employeetitle' + emp.Id + '" \>' + emp.Title + '</span>' +
                '<span class="col-xs-4" id=employeefname' + emp.Id + '" \>' + emp.Firstname + '</span>' +
                '<span class="col-xs-4" id=employeelastname' + emp.Id + '" \>' + emp.Lastname + '</span>'
            );
            btn.appendTo(div);
        });
    }//buildStudentList

    // get all students
    function getAll(msg) {
        $('#status').text('Finding Employee Information ...');

        ajaxCall('Get', 'api/employees', '')
            .done(function (data) {
                buildEmployeeList(data);
                if (msg === '')
                    $('#status').text('Employees Loaded');
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
        localStorage.removeItem('Id');
        localStorage.removeItem('DepartmentId');
        localStorage.removeItem('Timer');
    }

   
    function ajaxCall(type, url, data) {
        return $.ajax({ 
            type: type,
            url: url,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: true

        });

    } //ajaxCall

    function errorRoutine(jqXHR) { 
        if (jqXHR.responseJSON === null) {
            $('#status').text(jqXHR.responseText);
        }
        else {
            $('#status').text(jqXHR.responseText);
        }

    } //errorRoutine




});
