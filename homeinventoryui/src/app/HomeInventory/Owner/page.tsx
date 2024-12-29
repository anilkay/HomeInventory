"use client";
import React, {useMemo} from 'react';
import {useFetch} from "@/hooks/useFetch";
import {useRouter} from "next/navigation";
import {ColumnDef, createColumnHelper} from "@tanstack/react-table";
import {useTansactReactTable} from "@/hooks/useTansactReactTable";
interface Owner {
    id: number;
    name: string;
    surname: string;
    email: string;
    inventories: unknown | null;
}




export default  function OwnerPage() {
    const res=useFetch<Owner[]>("/HomeInventory/Owner")
    const router = useRouter();

    const owners = useMemo(() => {
        if(!res.data){
            return []
        }
        return res.data
    },[res.data])


    const columnHelper = createColumnHelper<Owner>();

    const handleViewDetails= (id:number)=> {
        if(router!==null){
        router.push(`/HomeInventory/Owner/${id}`);
    }
    else {
        console.log("router is null");
    }
    }

    const columns: ColumnDef<Owner>[] = [
        columnHelper.accessor("name",{
            header:"Name"
        }) as ColumnDef<Owner>,
        columnHelper.accessor("surname", {
            header:"Surname"
        }) as ColumnDef<Owner>,
        columnHelper.accessor("email",{
            header:"E-Mail"
        }) as ColumnDef<Owner>,
        columnHelper.display({
            header:"Details",
            id:"details",
            cell: (info) => {
                const id = info.row.original.id;
                return (
                    <button
                        onClick={() => handleViewDetails(id)}
                        className="bg-blue-500 hover:bg-blue-600 text-white py-1 px-3 rounded"
                    >
                        View Details
                    </button>
                );
            }
        })
    ]


    const reactTable= useTansactReactTable(columns, owners)


    if(res.loading){
        return <div>Loading...</div>;
    }

    if(res.error){
        return <div>Error: {res.error}</div>;
    }



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
                    onClick={navigateToAddNewOwner}
                    className="px-4 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 transition"
                >
                    Add Owner
                </button>
            </div>
            {
                reactTable
            }
        </div>
    );
}