import { useEffect, useState } from "react";

export const useFetchEffect = (asyncFunc = async () => { }, dependences = [], settings = { condition: false, handleSuccess: () => { }, handleError: () => { } }) => {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(false)
    const [toggle, toggleFetch] = useState([])


    const doFetch = () => {
        let newFetch = toggle
        newFetch = [...newFetch, {}]
        toggleFetch(newFetch)
    }


    useEffect(() => {

        let seguir = settings.condition
        if(!seguir) return;

        setLoading(true)
        asyncFunc()
            .then((result) => {
                console.log(result)
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
    }, dependences)

    return [loading, error, doFetch]
}