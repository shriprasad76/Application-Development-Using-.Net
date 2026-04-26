const apiBaseUrl = 'http://localhost:5000/api/vehicles';
let currentEditId = null;

const messageElement = document.getElementById('message');
const vehiclesBody = document.getElementById('vehicles-table-body');
const upcomingBody = document.getElementById('upcoming-table-body');
const vehicleForm = document.getElementById('vehicle-form');
const cancelEditButton = document.getElementById('cancel-edit');

async function fetchVehicles() {
    try {
        const response = await fetch(apiBaseUrl);
        const vehicles = await response.json();
        renderVehicles(vehicles);
    } catch (error) {
        showMessage('Error loading vehicles. Check backend connection.', 'danger');
    }
}

async function fetchUpcomingVehicles() {
    try {
        const response = await fetch(`${apiBaseUrl}/upcoming`);
        const vehicles = await response.json();
        renderUpcoming(vehicles);
    } catch (error) {
        showMessage('Error loading upcoming vehicles.', 'danger');
    }
}

function renderVehicles(vehicles) {
    vehiclesBody.innerHTML = '';
    const today = new Date();
    const limit = new Date(today);
    limit.setDate(limit.getDate() + 3);

    vehicles.forEach(vehicle => {
        const isUpcoming = isVehicleUpcoming(vehicle, today, limit);
        const row = document.createElement('tr');
        if (isUpcoming) {
            row.classList.add('table-danger');
        }

        row.innerHTML = `
            <td>${vehicle.customerName}</td>
            <td>${vehicle.mobileNumber}</td>
            <td>${vehicle.vehicleModel || ''}</td>
            <td>${vehicle.vehicleNumber || ''}</td>
            <td>${formatDate(vehicle.dateReceived)}</td>
            <td>${formatDate(vehicle.expectedReturnDate)}</td>
            <td>
                <button class="btn btn-sm btn-outline-primary me-1" onclick="editVehicle(${vehicle.id})">Edit</button>
                <button class="btn btn-sm btn-outline-danger" onclick="deleteVehicle(${vehicle.id})">Delete</button>
            </td>
        `;
        vehiclesBody.appendChild(row);
    });
}

function renderUpcoming(vehicles) {
    upcomingBody.innerHTML = '';
    if (vehicles.length === 0) {
        upcomingBody.innerHTML = '<tr><td colspan="2">No upcoming deliveries in 3 days.</td></tr>';
        return;
    }

    vehicles.forEach(vehicle => {
        const row = document.createElement('tr');
        row.classList.add('table-danger');
        row.innerHTML = `
            <td>${vehicle.customerName}</td>
            <td>${formatDate(vehicle.expectedReturnDate)}</td>
        `;
        upcomingBody.appendChild(row);
    });
}

function formatDate(dateString) {
    if (!dateString) return '';
    return dateString.slice(0, 10);
}

function isVehicleUpcoming(vehicle, today, limit) {
    const expected = new Date(vehicle.expectedReturnDate);
    return expected >= today && expected <= limit;
}

function getFormData() {
    return {
        customerName: document.getElementById('customer-name').value.trim(),
        mobileNumber: document.getElementById('mobile-number').value.trim(),
        vehicleModel: document.getElementById('vehicle-model').value.trim(),
        vehicleNumber: document.getElementById('vehicle-number').value.trim(),
        problemDescription: document.getElementById('problem-description').value.trim(),
        dateReceived: document.getElementById('date-received').value,
        expectedReturnDate: document.getElementById('expected-return-date').value,
    };
}

function validateForm(data) {
    if (!data.customerName || !data.mobileNumber || !data.dateReceived || !data.expectedReturnDate) {
        showMessage('Please fill in the required fields marked with *.', 'warning');
        return false;
    }

    const received = new Date(data.dateReceived);
    const expected = new Date(data.expectedReturnDate);
    if (expected < received) {
        showMessage('Expected return date cannot be earlier than date received.', 'warning');
        return false;
    }
    return true;
}

async function submitForm(event) {
    event.preventDefault();
    const formData = getFormData();

    if (!validateForm(formData)) {
        return;
    }

    try {
        if (currentEditId) {
            await updateVehicle(currentEditId, { id: currentEditId, ...formData });
        } else {
            await createVehicle(formData);
        }

        resetForm();
        await refreshData();
    } catch (error) {
        showMessage('Failed to save vehicle. Please try again.', 'danger');
    }
}

async function createVehicle(vehicle) {
    const response = await fetch(apiBaseUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(vehicle),
    });

    if (!response.ok) {
        throw new Error('Create failed');
    }

    showMessage('Vehicle record added successfully.', 'success');
}

async function updateVehicle(id, vehicle) {
    const response = await fetch(`${apiBaseUrl}/${id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(vehicle),
    });

    if (!response.ok) {
        throw new Error('Update failed');
    }

    showMessage('Vehicle record updated successfully.', 'success');
}

async function editVehicle(id) {
    try {
        const response = await fetch(apiBaseUrl);
        const vehicles = await response.json();
        const vehicle = vehicles.find(v => v.id === id);
        if (!vehicle) {
            showMessage('Vehicle not found for editing.', 'danger');
            return;
        }

        currentEditId = id;
        document.getElementById('customer-name').value = vehicle.customerName;
        document.getElementById('mobile-number').value = vehicle.mobileNumber;
        document.getElementById('vehicle-model').value = vehicle.vehicleModel || '';
        document.getElementById('vehicle-number').value = vehicle.vehicleNumber || '';
        document.getElementById('problem-description').value = vehicle.problemDescription || '';
        document.getElementById('date-received').value = formatDate(vehicle.dateReceived);
        document.getElementById('expected-return-date').value = formatDate(vehicle.expectedReturnDate);
        showMessage('Edit mode activated. Update the form and press Save.', 'info');
    } catch (error) {
        showMessage('Unable to load vehicle for edit.', 'danger');
    }
}

async function deleteVehicle(id) {
    if (!confirm('Are you sure you want to delete this vehicle record?')) {
        return;
    }

    try {
        const response = await fetch(`${apiBaseUrl}/${id}`, { method: 'DELETE' });
        if (!response.ok) {
            throw new Error('Delete failed');
        }
        showMessage('Vehicle record deleted successfully.', 'success');
        await refreshData();
    } catch (error) {
        showMessage('Could not delete vehicle record.', 'danger');
    }
}

function resetForm() {
    currentEditId = null;
    vehicleForm.reset();
    showMessage('Form reset. Enter a new vehicle record or select to edit.', 'secondary');
}

function showMessage(text, type) {
    messageElement.innerHTML = `<div class="alert alert-${type} alert-dismissible fade show" role="alert">
        ${text}
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
    </div>`;
}

async function refreshData() {
    await fetchVehicles();
    await fetchUpcomingVehicles();
}

vehicleForm.addEventListener('submit', submitForm);
cancelEditButton.addEventListener('click', resetForm);
window.addEventListener('DOMContentLoaded', refreshData);
