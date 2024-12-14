import React from 'react';

async function getOwners() {
    // Burada da gerçek bir API isteği yapabilirsiniz.
    // Örneğin:
    // const res = await fetch('http://localhost:3000/HomeInventory/Owner');
    // const data = await res.json();
    // return data;

    // Şimdilik örnek bir data dönüyoruz:
    return [
        { id: 'owner-001', name: 'John Doe' },
        { id: 'owner-002', name: 'Jane Smith' },
        { id: 'owner-003', name: 'Alice Johnson' },
    ];
}

export default async function OwnerPage() {
    const owners = await getOwners();

    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">Owner List</h1>
            <ul className="space-y-2">
                {owners.map((owner) => (
                    <li key={owner.id} className="p-4 bg-white rounded shadow">
                        <div className="font-semibold">{owner.name}</div>
                        <div className="text-gray-600">ID: {owner.id}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
}