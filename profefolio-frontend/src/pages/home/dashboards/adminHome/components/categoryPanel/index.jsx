import React from "react";
import styles from './index.module.css'

import { AiOutlineRightCircle } from 'react-icons/ai'

const CategoryPanel = ({
    text = "",
    handleClick = () =>{},
    children
}) => {
    return(
        <div className={styles.PanelContainer} onClick={handleClick}>
            <div className={styles.CPH}>
                <div className="d-flex justify-content-between align-items-center">
                    <h5 className={styles.Label}>{text}</h5>
                    <AiOutlineRightCircle className={styles.RightIcon} size={40}/>
                </div>
            </div>
            <div className={styles.CPB}>
                {children}
            </div>
        </div>
    )
}

export default CategoryPanel