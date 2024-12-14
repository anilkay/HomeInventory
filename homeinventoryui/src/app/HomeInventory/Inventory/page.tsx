"use client";

import React from 'react';
import {useFetch} from "@/hooks/useFetch";
import {useRouter} from "next/navigation";

interface InventoryResponse {
    inventory: {
        id: number;
        name: string;
        description: string;
        location: string | null;
        possibleValue: { id: number };
        ownerId: number | null;
        //owner: any; // Owner nesnesi gelirse burayı güncelleyebilirsiniz.
    };
    monetaryValue: {
        value: number;
        currency: string;
        id: number;
    };
    //otherValue: any; // OtherValue yapısına göre tipini güncelleyebilirsiniz
}




export default  function HomeInventoryPage() {
    const { data, loading, error, refetch } = useFetch<InventoryResponse[]>(`/HomeInventory/Inventory`);
    const router = useRouter();

    const handleViewDetails = (id: number) => {
        // Burada ayrıntı sayfasına yönlendirme yapıyoruz.
        if(router!==null){
            router.push(`/HomeInventory/Inventory/${id}`);
        }
        else {
            console.log("router is null");
        }
    };

    if(loading){
        return <div>Loading...</div>;
    }

    if(error){
        return <div>Error: {error}</div>;
    }

    if(data === null) {
        return <div>Inventory is Empty</div>;
    }


    return (
        <div>
            <h1 className="text-2xl font-bold mb-4">Inventory List</h1>
            <div className="flex gap-4">
                <button
                    onClick={refetch}
                    className="p-2 bg-blue-500 text-white rounded hover:bg-blue-600"
                >
                    Refresh Data
                </button>
                <button
                    onClick={() => router.push('/HomeInventory/Inventory/add')}
                    className="p-2 bg-green-500 text-white rounded hover:bg-green-600"
                >
                    Add New Inventory
                </button>
            </div>
            <ul className="space-y-2">
                {data.map((item) => (
                    <li key={item.inventory.id} className="p-4 bg-white rounded shadow">
                        <div className="font-semibold">{item.inventory.name}</div>
                        <div className="text-gray-600">ID: {item.inventory.location}</div>
                        <button
                            onClick={() => handleViewDetails(item.inventory.id)}
                            className="mt-2 inline-block bg-blue-500 hover:bg-blue-600 text-white py-1 px-3 rounded"
                        >
                            View Details
                        </button>
                    </li>
                ))}
            </ul>
        </div>
    );
}