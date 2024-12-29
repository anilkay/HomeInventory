import {ColumnDef, getCoreRowModel, flexRender, useReactTable} from "@tanstack/react-table";
import React from "react";

export function useTansactReactTable <TableModel= unknown>(columns: ColumnDef<TableModel>[],data:TableModel[] )
{
    const table = useReactTable({
        data:data || [],
        columns:columns,
        getCoreRowModel: getCoreRowModel(),
    })

    return (
        <table className="table-auto w-full border-collapse border border-gray-300">
            <thead>
            {table.getHeaderGroups().map((headerGroup) => (
                <tr key={headerGroup.id}>
                    {headerGroup.headers.map((header) => (
                        <th
                            key={header.id}
                            className="border-b border-gray-300 p-2 text-left"
                        >
                            {header.isPlaceholder
                                ? null


                                : header.column.id}
                        </th>
                    ))}
                </tr>
            ))}
            </thead>
            <tbody>
            {table.getRowModel().rows.map(row => (
                <tr key={row.id}>
                    {row.getVisibleCells().map(cell => (
                        <td key={cell.id}>
                            {flexRender(cell.column.columnDef.cell, cell.getContext())}
                        </td>
                    ))}
                </tr>
            ))}
            </tbody>
        </table>
    )

}
