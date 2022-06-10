var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadButtonsByRoles(data) {
    if ($("#role").text() == "Support") {
        return `
                    <div class="w-100 btn-group=" role="group" >
                            <a href="/Admin/Assignation/Upsert?id=${data.id}"
                            class="btn btn-primary mr-1"><i class="bi bi-pencil-square"></i> Edit </a>
                             <a onClick =Delete('/Admin/Assignation/Delete/${data.id}')
                            class="btn btn-danger mr-1"><i class="bi bi-trash3-fill"></i> Delete </a>
                        </div>
                        `
    } else if ($("#role").text() == "Admin") {
        switch (data.request.status) {
            case "En proceso":
                return `
                    <div class="w-100 btn-group=" role="group" >
                            <a href="/Admin/Assignation/EndTicket?id=${data.id}"
                            class="btn btn-primary mr-1"><i class="bi bi-pencil-square"></i> Finalizar </a>
                        </div>
                        `
                break;
            case "Pendiente":
                return `
                    <div class="w-100 btn-group=" role="group" >
                            <a href="/Admin/Assignation/EndTicket?id=${data.id}"
                            class="btn btn-primary mr-1"><i class="bi bi-pencil-square"></i> Comenzar </a>
                        </div>
                        `
                break;
            default:
        }

        
    }
        
}

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url": "/Admin/Assignation/GetAll"

        },
        "columns": [
            { "data": "request.description", "width": "40%" },
            { "data": "request.status", "width": "20%" },
            { "data": "applicationUser.name", "width": "20%" },

            {
                "data": null,
                "render": function (data) {
                    return loadButtonsByRoles(data);
                }, 
                "width": "200%"
            }

        ]
    });
}
function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    }
                    else {
                        toastr.error(data.message);
                    }
                }
            })
        }
    })
}