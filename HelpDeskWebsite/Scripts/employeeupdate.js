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
                    $('#status').text('employee found');
                    localStorage.setItem('Id', data.Id);
                    localStorage.setItem('DepartmentId', data.DepartmentId);
                    localStorage.setItem('Timer', data.Timer);
                } else {
                    $('#TextBoxFirstname').val('not found');
                    $('#TextBoxLastname').val('');
                    $('#TextBoxEmail').val('');
                    $('#TextBoxTitle').val('');
                    $('#TextBoxPhone').val('');
                    $('#status').text('no such employee');
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
        ajaxCall('put', 'api/employees', emp)
            .done(function (data) {
                $('#status').text(data);
            })
            .fail(function (jqXHR, textStatus, errorThrown) {
                errorRoutine(jqXHR);
            });
        return false; 


    }); //update button click 


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

