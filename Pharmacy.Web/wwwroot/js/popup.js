function ConfirmDeleteCategory(event) {
    if (!confirm('Are you sure you want to delete this Category?')) {
        event.preventDefault(); 
        return false;
    }
    return true;
}

function ConfirmDeleteFactory(event) {
    if (!confirm('Are you sure you want to delete this Factory?')) {
        event.preventDefault(); 
        return false;
    }
    return true;
}

function ConfirmDeleteIngredient() {
    if (!confirm('Are you sure you want to delete this Ingredient?')){
        event.preventDefault(); 
        return false;
    }
    return true;
}

function ConfirmDeleteMedicine() {
    if (!confirm('Are you sure you want to delete this Medicine?')) {
        event.preventDefault(); 
        return false;
    }
    return true;
}

function ConfirmDeletePrescription() {
    if (!confirm('Are you sure you want to delete this Prescription?')) {
        event.preventDefault(); 
        return false;
    }
    return true;
}



