$(function () {
    //get grab the data 
    getAll(''); 


    //click event handler 
    $('#employeeList').click(function (e) {  //click on any row
        if (!e) e = window.event;
        var Id = e.target.parentNode.id;
        if (Id === 'employeeList' || Id === '') {
             //clicked on row somehwere else
            Id = e.target.id;

        }
        var data = JSON.parse(localStorage.getItem('allemployees'));
        clearModalFields();

        //checking we gonna do  an add or update/delete?
        if (Id === '0') {
            setupForAdd();

        } else {
            setupForUpdate(Id, data);

        }


    });


        // actionbutton

    $('#actionbutton').click(function () {
        if ($('#actionbutton').val() === 'Update') {
            update();
        }
        else {
            add();

        }
        return false;
    }); //actionbutton click


   // delete button 
    $('#deletebutton').click(function () {
        var deleteEmployee = confirm('really delete this employee?');
        if (deleteEmployee) {
            _delete();
            return !deleteEmployee;
        }
        else
            return deleteEmployee;
    }); //deletebutton click

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
                // departments
                loadDepartmentDDL(employee.DepartmentId);
                return false; 

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
    function buildEmployeeList(data, allemployees) {
        $('#employeeList').empty();
        div = $('<div class="list-group"><div>' +
            '<span class="col-xs-4 h3">Title</span>' +
            '<span class="col-xs-4 h3">First</span>' +
            '<span class="col-xs-4 h3">Last</span>' +
            '</div>');
        div.appendTo($('#employeeList'))


        if (allemployees) {
            localStorage.setItem('allemployees', JSON.stringify(data));
        }


      
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


        }); 
    } //buildEmployeeList



    //get all employees
    function getAll(msg) {
        $('#status').text('Finding Employee Information...');


        ajaxCall('Get', 'api/employees', '')
            .done(function (data) {
                buildEmployeeList(data,true);
                if (msg === '')
                    $('#status').text('Employees loaded');
                else
                    $('#status').text(msg + ' - Employees Loaded');

            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);

            });

        //departments
        ajaxCall('Get', 'api/department', '')
            .done(function (data) {
                localStorage.setItem('alldepartments', JSON.stringify(data));
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                alert('error');
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
        //added for departments
        loadDepartmentDDL(-1);

    }

    function add() {
        emp = new Object();
        emp.Title = $('#TextBoxTitle').val();
        emp.Firstname = $('#TextBoxFirstname').val();
        emp.Lastname = $('#TextBoxLastname').val();
        emp.Phoneno = $('#TextBoxPhone').val();
        emp.Email = $('#TextBoxEmail').val();
        //emp.DepartmentId = 100; //we'll hardcode now, add a drop down later
        //added for departments
        emp.DepartmentId = $('#ddlDept').val();


        ajaxCall('Post', 'api/employees', emp)
            .done(function (data) {
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });

        $('#theModal').modal('toggle');
        return false; 

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
        //added  departments
        emp.DepartmentId = $('#ddlDept').val();


        ajaxCall('put', 'api/employees', emp)
            .done(function (data) {
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });

        $('#theModal').modal('toggle');
        return false; 



    }//update

    
    function _delete() {
        ajaxCall('Delete', 'api/employees/' + localStorage.getItem('Id'), '')
            .done(function (data) {
                getAll(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        $('#theModal').modal('toggle');
    } //_delete

  
    function loadDepartmentDDL(empdep) {
        html = '';
        $('#ddlDept').empty();
        var alldepartments = JSON.parse(localStorage.getItem('alldepartments'));
        $.each(alldepartments, function () {
            html += '<option value="' + this['Id'] + '">' + this['Name'] + '</option>';
        });
        $('#ddlDept').append(html);
        $('#ddlDept').val(empdep);
    }//loadDepartmentDDL

    $("#srch").keyup(function () {
        filterData();

    });


    function filterData() {
        filteredData = [];
        allData = JSON.parse(localStorage.getItem('allemployees'));

        $.each(allData, function (n, i) {
            if (~i.Lastname.indexOf($("#srch").val())) {
                filteredData.push(i);
            }
        })

        buildEmployeeList(filteredData, false);
    }//fillterdata

    //ajax call
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



    function errorRoutine(jqXHR) { 
        if (jqXHR.responseJSON === null) {
            $('#status').text(jqXHR.responseText);
        }
        else {
            $('#status').text(jqXHR.responseText);
        }

    } //errorRoutine
});
