"use client";

import React, { useState, FormEvent } from 'react';
import { useRouter } from 'next/navigation';

export default function AddInventoryPage() {
    const router = useRouter();

    const [name, setName] = useState('');
    const [description, setDescription] = useState('');
    const [locationName, setLocationName] = useState('');
    const [address, setAddress] = useState('');
    const [value, setValue] = useState('');
    const [currency, setCurrency] = useState('');

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        const body = {
            name,
            description,
            locationName,
            address,
            monetaryValueDto: {
                value: Number(value),
                currency
            }
        };

        const res = await fetch('http://localhost:5298/HomeInventory/Inventory/WithAddressWithMonetaryValue', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(body),
        });

        if (res.ok) {
            router.push('/HomeInventory/Inventory');
        } else {
            console.error('Error adding inventory item');
        }
    };

    return (
        <div className="p-4 space-y-6">
            <h1 className="text-2xl font-bold mb-4">Add New Inventory</h1>
            <form onSubmit={handleSubmit} className="grid grid-cols-2 gap-4 p-4 bg-white rounded shadow">
                <div>
                    <label className="block mb-1 font-semibold">Name:</label>
                    <input
                        className="border p-2 w-full"
                        value={name}
                        onChange={(e) => setName(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label className="block mb-1 font-semibold">Description:</label>
                    <input
                        className="border p-2 w-full"
                        value={description}
                        onChange={(e) => setDescription(e.target.value)}
                    />
                </div>

                <div>
                    <label className="block mb-1 font-semibold">Location Name:</label>
                    <input
                        className="border p-2 w-full"
                        value={locationName}
                        onChange={(e) => setLocationName(e.target.value)}
                    />
                </div>

                <div>
                    <label className="block mb-1 font-semibold">Address:</label>
                    <input
                        className="border p-2 w-full"
                        value={address}
                        onChange={(e) => setAddress(e.target.value)}
                    />
                </div>

                <div>
                    <label className="block mb-1 font-semibold">Value:</label>
                    <input
                        className="border p-2 w-full"
                        type="number"
                        value={value}
                        onChange={(e) => setValue(e.target.value)}
                        required
                    />
                </div>

                <div>
                    <label className="block mb-1 font-semibold">Currency:</label>
                    <input
                        className="border p-2 w-full"
                        value={currency}
                        onChange={(e) => setCurrency(e.target.value)}
                        required
                    />
                </div>

                <div className="col-span-2 text-right">
                    <button
                        type="submit"
                        className="bg-green-500 hover:bg-green-600 text-white py-2 px-4 rounded"
                    >
                        Add
                    </button>
                </div>
            </form>
        </div>
    );
}