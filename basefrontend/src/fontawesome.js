// src/fontawesome.js
import { library } from '@fortawesome/fontawesome-svg-core'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'
import { faUser, faLock, faSignInAlt } from '@fortawesome/free-solid-svg-icons'

// Add icons to the library
library.add(faUser, faLock, faSignInAlt)

export default FontAwesomeIcon
