@model IEnumerable<fitness_app.Models.Trainer>

@{
    ViewData["Title"] = "Antrenörler";
}

<div class="container mt-5">
    <div class="d-flex justify-content-between align-items-center mb-4">
        <h2>Antrenör Kadrosu</h2>

        @if (User.IsInRole("Admin"))
        {
            <a asp-action="Create" class="btn btn-primary">+ Yeni Antrenör Ekle</a>
        }
    </div>

    <div class="row">
        @foreach (var item in Model)
        {
            <div class="col-md-4 mb-4">
                <div class="card shadow h-100">
                    <img src="@item.ImageUrl" class="card-img-top" alt="@item.FullName" style="height: 250px; object-fit: cover;">

                    <div class="card-body">
                        <h5 class="card-title">@item.FullName</h5>

                        @if (item.GymService != null)
                        {
                            <span class="badge bg-info text-dark mb-2">@item.GymService.Name Uzmanı</span>
                        }

                        <p class="card-text text-muted">
                            <small>Uzmanlık: @item.ExpertiseArea</small><br />
                            <strong>Mesai:</strong> @item.StartTime.ToString(@"hh\:mm") - @item.EndTime.ToString(@"hh\:mm")
                        </p>
                    </div>

                    @if (User.IsInRole("Admin"))
                    {
                        <div class="card-footer bg-white border-top-0 d-grid gap-2">
                            <a asp-action="Edit" asp-route-id="@item.Id" class="btn btn-warning btn-sm">Bilgileri Düzenle</a>
                            <a asp-action="Delete" asp-route-id="@item.Id" class="btn btn-outline-danger btn-sm" onclick="return confirm('Silmek istediğine emin misin?');">Antrenörü Sil</a>
                        </div>
                    }
                </div>
            </div>
        }

        @if (!Model.Any())
        {
            <div class="col-12">
                <div class="alert alert-warning">Sistemde kayıtlı antrenör bulunmamaktadır.</div>
            </div>
        }
    </div>
</div>
