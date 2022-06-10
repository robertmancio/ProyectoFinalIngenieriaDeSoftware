var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            "url":"/Admin/Request/GetAll"
        },
        "columns": [
            { "data": "requirement.name", "width": "20%" },
            { "data": "description", "width": "40%" },
            { "data": "status", "width": "20%" },
            {
                "data": "id",
                "render": function (data) {
                    return `
                    <div class="w-100 btn-group=" role="group" >
                            <a href="/Admin/Request/Upsert?id=${data}"
                            class="btn btn-primary mr-1"><i class="bi bi-pencil-square"></i> Edit </a>
                             <a onClick =Delete('/Admin/Request/Delete/${data}')
                            class="btn btn-danger mr-1"><i class="bi bi-trash3-fill"></i> Delete </a>
                        </div>
                        `
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