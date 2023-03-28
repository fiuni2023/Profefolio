import axios from 'axios';
import React, { useEffect, useState } from 'react'
import APILINK from '../../../components/link';

const useAxiosGet = (url = "", token = "") => {

    const [data, setData] = useState(null);
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);


    useEffect(() => {
        axios.get(`${APILINK}/${url}`, {
            headers: {
                Authorization: `Bearer ${token}`,
            }
        })
            .then(response => {

                setData(response.data)
                setLoading(false)
                setError(null)

            })
            .catch(er => {
                setData(null)
                setLoading(false)
                setError(er)
                console.error(er);
            });
    }, [url])
    return [data, loading, error, setData];
}

export default useAxiosGet