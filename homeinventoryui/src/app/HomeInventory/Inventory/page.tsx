"use client";

import React, {useMemo} from 'react';
import {useFetch} from "@/hooks/useFetch";
import {useRouter} from "next/navigation";
import {ColumnDef, createColumnHelper, flexRender, getCoreRowModel, useReactTable} from '@tanstack/react-table';
import {useTansactReactTable} from "@/hooks/useTansactReactTable";

interface Inventory {
    id: number;
    name: string;
    description: string;
    location: string | null;
    possibleValue: { id: number };
    ownerId: number | null;
    //owner: any; // Owner nesnesi gelirse burayı güncelleyebilirsiniz.
}

interface MonetoryValue {
    value: number;
    currency: string;
    id: number;
}

interface InventoryResponse {
    inventory: Inventory
    monetaryValue: MonetoryValue
    //otherValue: any; // OtherValue yapısına göre tipini güncelleyebilirsiniz
}




export default  function HomeInventoryPage() {
    const { data, loading, error, refetch } = useFetch<InventoryResponse[]>(`/HomeInventory/Inventory`);
    const router = useRouter();

    const inventories = useMemo(() => {
        if(!data){
            return [];
        }
        return data
    },[data])

    const handleViewDetails = (id: number) => {
        if(router!==null){
            router.push(`/HomeInventory/Inventory/${id}`);
        }
        else {
            console.log("router is null");
        }
    };

    const columnHelper = createColumnHelper<InventoryResponse>();




    const columns: ColumnDef<InventoryResponse>[] = [
        columnHelper.accessor('inventory.id', {
            header: 'ID',
            cell: (info) => info.getValue(),
        }) as ColumnDef<InventoryResponse>
        ,
        columnHelper.accessor('inventory.name', {
            header: 'Name',
            cell: (info) => info.getValue(),
        }) as ColumnDef<InventoryResponse>
        ,
        columnHelper.accessor("inventory.description",
            {
                header: 'Location',
                cell: (info) => <span className="text-gray-600">{String(info.getValue())}</span>,
            }
            ) as ColumnDef<InventoryResponse>,


        columnHelper.display({
            id: 'details',
            header:()=> 'Details',
            cell: (info) => {
                const inventory = info.row.original.inventory;
                return (
                    <button
                        onClick={() => handleViewDetails(inventory?.id)}
                        className="bg-blue-500 hover:bg-blue-600 text-white py-1 px-3 rounded"
                    >
                        View Details
                    </button>
                );
            },
        }) as ColumnDef<InventoryResponse>,
    ];

    const reactTable=useTansactReactTable(columns, inventories)



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
            {
                reactTable
            }
        </div>
    );
}