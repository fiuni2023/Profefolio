import { useEffect, useState } from "react";

export const useFetchEffect = (asyncFunc = async () => { }, dependences = [], settings = { condition: false, handleSuccess: () => { }, handleError: () => { } }) => {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(false)
    const [toggle, toggleFetch] = useState([])


    const doFetch = () => {
        toggleFetch((prev)=>[...prev, {}])
    }


    useEffect(() => {

        let seguir = settings.condition
        if(!seguir) return;

        setLoading(true)
        asyncFunc()
            .then((result) => {
                setLoading(false)
                settings.handleSuccess(result)
            })
            .catch((error) => {
                setError(true)
                settings.handleError(error)
            })
            .finally(() => {
                setLoading(false)
            })

        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [...dependences, toggle])

    return {loading, error, doFetch}
}