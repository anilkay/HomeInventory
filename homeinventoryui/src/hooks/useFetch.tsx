"use client";

import {useState, useEffect, useCallback} from 'react';

// Bu baseUrl'i istediğiniz yere göre düzenleyebilirsiniz.
// Örneğin env değişkenlerinden almayı tercih edebilirsiniz.
const baseUrl = process.env.NEXT_PUBLIC_API_BASE_URL || 'http://localhost:5298';





export function useFetch<T = unknown>(endpoint: string) {
    const [data, setData] = useState<T | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    const fetchData = useCallback(async () => {
        try {
            setLoading(true);
            const res = await fetch(`${baseUrl}${endpoint}`);
            if (!res.ok) {
                throw new Error(`Fetch error: ${res.status} ${res.statusText}`);
            }
            const json = await res.json();
            setData(json);
            setError(null);
        } catch (err: any) {
            setError(err.message);
        } finally {
            setLoading(false);
        }
    }, [endpoint]);

    useEffect(() => {
        fetchData();
    }, [fetchData]);

    // refetch fonksiyonu manuel olarak fetchData çağırmanızı sağlar
    const refetch = () => fetchData();

    return { data, loading, error, refetch };
}