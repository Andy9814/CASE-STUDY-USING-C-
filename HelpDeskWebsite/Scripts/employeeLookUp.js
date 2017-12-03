$(function () {
    $('#getbutton').click(function (e) {
        var lastname = $('#TextBoxLastname').val();
        $('#status').text('please wait....');
        ajaxCall('Get', 'api/employee/' + lastname, ' ')
            .done(function (data)// actually see ajax.done();
        {
            if (data.Lastname !== 'not found')
            {
                $('#Email').text(data.Email);
                $('#title').text(data.Title);
                $('#firstname').text(data.Firstname);
                $('#phone').text(data.Phoneno);
                $('#status').text('employee found');
            } else
            {
                $('#firstname').text('not found');
                $('#Email').text('');
                $('#title').text('');
                $('#phone').text('');
                $('#status').text('no such Employee');
            }
        })
            .fail(function (jqXHR, textStatus, errorThrown) 
            {
                errorRoutine(jqXHR);
            });
    });
    function ajaxCall(type, url, data) {
        return $.ajax({// every ajax returns a promise 
           // method: type,//pst,Get,put,delete
            type: type,
            url: url,
            data: JSON.stringify(data),
            contentType: 'application/json; charset=utf-8',
            dataType: 'json',
            processData: true
        });
    }
    function errorRoutine(jqXHR)
    {
        if (jqXHR.responseJSON == null)
        {
            $('#status').text(jqXHR.responseText);
        }
        else
        {
            $('#status').text(jqXHR.responseJSON.Message);
        }
    }
});