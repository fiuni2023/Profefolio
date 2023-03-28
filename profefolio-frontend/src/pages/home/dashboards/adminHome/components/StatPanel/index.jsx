import React from "react";
import styles from './index.module.css'

const StatPanel = ({
    data = ""
}) => {
    return(
        <div className="d-flex justify-content-between">
            {data}
        </div>
    )
}

export default StatPanel