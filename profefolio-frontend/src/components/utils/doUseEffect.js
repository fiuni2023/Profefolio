import { useEffect, useState } from "react";

export const doUseEffect = ( asyncFunc = async() => {}, dependences = [], settings = {condition:() => {return true}, handleSuccess:()=>{}, handleError: ()=>{}} ) => {
    const [loading, setLoading] = useState(false)
    const [error, setError] = useState(false)
    const [ _, toggleFetch] = useState(false)

    const doFetch = () => {
        toggleFetch((prev) => !prev)
    }

    useEffect(()=>{
        setLoading(true)
        if(settings.condition){
            asyncFunc()
            .then((result)=>{
                setLoading(false)
                handleSuccess(result)
            })
            .catch((error)=>{
                setError(true)
                handleError(error)
            })
        }
    }, dependences)

    return [loading, error, doFetch]
}