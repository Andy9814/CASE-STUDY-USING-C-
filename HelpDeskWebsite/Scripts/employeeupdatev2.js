$(function () {
    $('#getbutton').click(function (e) {
        var lastname = $('#TextBoxFindLastname').val();
        $('#status').text('please wait....');
        ajaxCall('Get', 'api/employee/' + lastname, ' ')
            .done(function (data)// actually see ajax.done();
            {
                if (data.Lastname !== 'not found') {
                    $('#TextBoxEmail').val(data.Email);
                    $('#TextBoxTitle').val(data.Title);
                    $('#TextBoxFirstname').val(data.Firstname);
                    $('#TextBoxLastname').val(data.Lastname);
                    $('#TextBoxPhone').val(data.Phoneno);
                    $('#ImageHolder').html('<img height = "120" width = "110" src="data:image/png;base64,' + data.Picture64 + '"/>');

                    $('#status').text('employee found');
                    localStorage.setItem('Id', data.Id);
                    localStorage.setItem('DepartmentId', data.DepartmentId);
                    localStorage.setItem('Timer', data.Timer);
                    localStorage.setItem('EmployeePicture', data.Picture64);
                } else {
                    $('#TextBoxFirstname').val('not found');
                    $('#TextBoxLastname').val('');
                    $('#TextBoxEmail').val('');
                    $('#TextBoxTitle').val('');
                    $('#TextBoxPhone').val('');
                    $('#status').text('no such employee');
                    $('#ImageHolder').html('');
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);

            }); //ajaxCall
        $("#theModal").modal("toggle");
    }); //get button



    $('#updatebutton').click(function (e) {
        emp = new Object();
        emp.Title = $("#TextBoxTitle").val();
        emp.Firstname = $("#TextBoxFirstname").val();
        emp.Lastname = $("#TextBoxLastname").val();
        emp.Phoneno = $("#TextBoxPhone").val();
        emp.Email = $("#TextBoxEmail").val();
        emp.Id = localStorage.getItem("Id");
        emp.DepartmentId = localStorage.getItem("DepartmentId");
        emp.Timer = localStorage.getItem('Timer');

        if (localStorage.getItem('EmployeePicture')) {
            emp.Picture64 = localStorage.getItem('EmployeePicture');
        }
        ajaxCall('put', 'api/employees', emp)
            .done(function (data) {
                $('#status').text(data);

                if (data.indexOf('not') === -1) {
                    $('#ImageHolder').html('<img height = "120" width = "110" src="data:image/png;base64,' +
                        localStorage.getItem('EmployeePicture') + '"/>');
                }
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                $("#myModal").modal("toggle");
                errorRoutine(jqXHR);
            });
        return false;


    }); //update button click 


    $("input:file").change(() => { //click event handler
        var reader = new FileReader();
        var file = $('#fileUpload')[0].files[0];

        if (file) {
            reader.readAsBinaryString(file);
        }

        reader.onload = function (readerEvt) {

            var binaryString = reader.result;
            var encodedString = btoa(binaryString);
            localStorage.setItem('EmployeePicture', encodedString);
        }


    });

    function ajaxCall(type, url, data) {
        return $.ajax({ //return the promise that '$.ajax' returns
            type: type,
            url: url,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: true

        });
    } //ajaxCall


    function errorRoutine(jqXHR) {
        if (jqXHR.repsonseJSON == null) {
            $('#status').text(jqXHR.responseText);
        }
        else {
            $('#status').text(jqXHR.responseJSON.Message);
        }

    }//errorRoutine

});

