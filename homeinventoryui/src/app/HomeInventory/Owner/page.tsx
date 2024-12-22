"use client";
import React from 'react';
import {useFetch} from "@/hooks/useFetch";
import {router} from "next/client";
import {useRouter} from "next/navigation";
interface Owner {
    id: number;
    name: string;
    surname: string;
    email: string;
    inventories: any | null;
}




export default  function OwnerPage() {
    const res=useFetch<Owner[]>("/HomeInventory/Owner")
    const router = useRouter();

    if(res.loading){
        return <div>Loading...</div>;
    }

    if(res.error){
        return <div>Error: {res.error}</div>;
    }

    const owners = res.data;

    if(!owners){
        return <div>Error: {owners}</div>;
    }

    function navigateToAddNewOwner(){
        router.push('/HomeInventory/Owner/add')
    }

    return (
        <div className="max-w-4xl mx-auto p-6 bg-gray-50 rounded-lg shadow-lg">
            <div className="flex justify-between items-center mb-6">
                <h1 className="text-3xl font-bold text-gray-800">Owner List</h1>
                <button
                    onClick={navigateToAddNewOwner} // Butona tıklama fonksiyonu
                    className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
                >
                    Add Owner
                </button>
            </div>
            <ul className="divide-y divide-gray-200">
                {owners.map((owner) => (
                    <li key={owner.id} className="p-4 bg-white rounded-lg shadow hover:shadow-lg transition-shadow">
                        <div className="text-lg font-semibold text-gray-800">Name: {owner.name}</div>
                        <div className="text-gray-600">Surname: {owner.surname}</div>
                        <div className="text-gray-600">Email: {owner.email}</div>
                    </li>
                ))}
            </ul>
        </div>
    );
}