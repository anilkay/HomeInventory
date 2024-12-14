"use client";

import React from 'react';
import { useRouter, useParams } from 'next/navigation';
import {useFetch} from "@/hooks/useFetch";

interface InventoryResponse {
    inventory: {
        id: number;
        name: string;
        description: string | null;
        location: string | null;
        possibleValue: { id: number };
        ownerId: number | null;
    };
    monetaryValue: {
        value: number;
        currency: string;
        id: number;
    } | null;
}

export default function InventoryDetailPage() {
    const params = useParams();
    const router = useRouter();

    // ID’yi alıyoruz. Örneğin URL: /HomeInventory/Inventory/5 ise params.id = "5"
    const { data, loading, error, refetch } = useFetch<InventoryResponse>(`/HomeInventory/Inventory/${params.id}`);

    if (loading)
    {
        return <div>Loading inventory details...</div>;
    }

    if (error){
        return <div>Error: {error}</div>;
    }

    if (!data){
        return <div>Error: {error}</div>;
    }

    const { inventory, monetaryValue } = data;

    return (
        <div className="p-4 bg-white shadow rounded">

            <h1 className="text-2xl font-bold mb-4">Inventory Details</h1>
            <div className="mb-2">
                <span className="font-semibold">ID:</span> {inventory.id}
            </div>
            <div className="mb-2">
                <span className="font-semibold">Name:</span> {inventory.name}
            </div>
            <div className="mb-2">
                <span className="font-semibold">Description:</span> {inventory.description ?? 'N/A'}
            </div>
            <div className="mb-2">
                <span className="font-semibold">Location:</span> {inventory.location ?? 'N/A'}
            </div>
            <div className="mb-2">
                <span className="font-semibold">Possible Value ID:</span> {inventory.possibleValue?.id ?? 'N/A'}
            </div>
            <div className="mb-2">
                <span className="font-semibold">Owner ID:</span> {inventory.ownerId ?? 'N/A'}
            </div>


            {monetaryValue && (
                <div className="mb-2 mt-4 p-4 bg-gray-100 rounded">
                    <h2 className="text-xl font-semibold mb-2">Monetary Value</h2>
                    <div><span className="font-semibold">Value:</span> {monetaryValue.value}</div>
                    <div><span className="font-semibold">Currency:</span> {monetaryValue.currency}</div>
                    <div><span className="font-semibold">ID:</span> {monetaryValue.id}</div>
                </div>
            )}



            <div className="mt-4 flex gap-2">
                <button
                    onClick={() => router.back()}
                    className="bg-gray-300 hover:bg-gray-400 text-black py-1 px-3 rounded"
                >
                    Back
                </button>
                <button
                    onClick={refetch}
                    className="bg-blue-500 hover:bg-blue-600 text-white py-1 px-3 rounded"
                >
                    Refresh Data
                </button>
            </div>
        </div>
    );
}