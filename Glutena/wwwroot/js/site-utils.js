// Türkçe karakterleri düzelt ve slug oluştur
function generateSlug(text) {
    return text
        .toLowerCase()
        .replace(/ğ/g, 'g')
        .replace(/ü/g, 'u')
        .replace(/ş/g, 's')
        .replace(/ı/g, 'i')
        .replace(/ö/g, 'o')
        .replace(/ç/g, 'c')
        .replace(/[^a-z0-9\s-]/g, '')
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-')
        .trim();
}

// Slug input'unu otomatik doldur
function bindSlugInput(nameInputId, slugInputId) {
    var nameInput = document.getElementById(nameInputId);
    var slugInput = document.getElementById(slugInputId);
    if (nameInput && slugInput) {
        nameInput.addEventListener('input', function () {
            slugInput.value = generateSlug(this.value);
        });
    }
}