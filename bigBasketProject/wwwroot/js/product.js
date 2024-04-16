var dataTable;

$(document).ready(function () {
    loadDataTable();
});


function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/product/getall' },
        "columns": [
            { data: 'productName' },
            { data: 'mrp' },
            { data: 'price' },
            { data: 'offer' },
            { data: 'productDescription' },
            { data: 'aboutProduct' },
            { data: 'benefit' },
            { data: 'uses' },
            { data: 'category.name' },
            {
                data: 'productId',
                "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                    <a href="/admin/product/upsert/${data}" class="btn btn-dark mx-1"><i class="bi bi-pencil-square"></i> Edit</a>
                    <a onClick= Delete('/admin/product/delete/${data}') class="btn btn-danger mx-1"><i class="bi bi-trash-fill"></i> Delete</a>
                    </div>`
                }
                
            }
            
        ],
        "scrollX": true, // Enable horizontal scrolling
        "autoWidth": true // Allow DataTables to adjust column width automatically
    });
}

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}