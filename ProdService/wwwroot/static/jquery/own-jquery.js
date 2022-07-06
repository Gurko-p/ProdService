

$('#searchFIO').keyup(function (e) {
    e.preventDefault();
    $('.table tbody').empty();
    $.get("/Home/filterEmployees", { fio: $('#searchFIO').val() }, function (data) {
        console.log(data);
        fillTable(data);
    });
});

function fillTable(data) {
    for (var i = 0; i < data.length; i++) {
        $('.table tbody').append(`<tr class="text-center"><td>${i + 1}</td>
                                             <td><a href="/Home/UpdateEmployee?key=${data[i].id}">${data[i].fio}</a></td>
                                                 <td>${data[i].subdivision.subdivisionName}</td>
                                                 <td>${data[i].rank.rankName}</td>
                                                 <td>${data[i].position.positionName}</td>
                                                 <td>${data[i].countCourseVaccination}</td>
                                                 <td class="td-control">
                                                    <form asp-action="DeleteEmployee" method="post">
                                                        <input type="hidden" name="Id" value="${data[i].id}" />
                                                        <button type="submit" class="btn btn-outline-danger">
                                                            Удалить
                                                        </button>
                                                        <a class="btn btn-outline-success"
                                                           href="/Vaccination/VaccinationCourses?id=${data[i].id}">
                                                                Курсы вакцинации
                                                            </a>
                                                    </form></td>
                                                </tr>`);
    }
};