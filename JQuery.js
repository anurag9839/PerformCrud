$(document).ready(function () {
    alert("ok");
    $('#dtBasicExample').DataTable();
    $('.dataTables_length').addClass('bs-select');


    $("#AddStModal").click(function () {
        $.getJSON("/home/BindTeacherDDL", null, function (ddl) {
            var ddlteacher = "<option value=''>Select Teacher</option>"
            $.each(ddl, function (i, ddl) {
                ddlteacher = ddlteacher + "<option value='" + ddl.Id + "'>" + ddl.First_Name + " " + ddl.Last_Name + "</option>";

            })
            $("#DDlTeacher").html(ddlteacher);
        })
    })

    $(".Edit").click(function () {
        var StId = $(this).attr("data-id");
        alert(StId)
        $.getJSON("/Home/GetStudentDetailsToUpdate", { Id: StId }, function (res) {
            if (res) {
                $.getJSON("/home/BindTeacherDDL", null, function (tech) {
                
                    var ddlteacher = " < option value = '' > Select Teacher</option>";
                    $.each(tech, function (i, ddl) {
                    
                        if (res.Teacher_Id == ddl.Id) {
                         
                            ddlteacher = ddlteacher + "<option selected value='" + ddl.Id + "'>" + ddl.First_Name + " " + ddl.Last_Name + "</option>";
                        }
                        else
                            ddlteacher = ddlteacher + "<option value='" + ddl.Id + "'>" + ddl.First_Name + " " + ddl.Last_Name + "</option>";

                    })
                    $("#DDlTeacher2").html(ddlteacher);
                })
                
                $("#stuid").val(res.id);
                $("#StuFirstName").val(res.First_Name)
                $("#StuLastName").val(res.last_Name)
                $("#DOB").val(res.Date_of_Birth)

                if (res.Gender == "Male")
                    $("#rdbmale").attr("checked", true)
                else
                    $("#rdbfemale").attr("checked", true)
            }
        })
   
    })
});