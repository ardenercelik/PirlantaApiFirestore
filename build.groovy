pipeline {
    agent any
    environment { 
        CC = 'clang'
    }
    stages {
    stage ('Clean workspace') {
      steps {
        cleanWs()
      }
    }   
  }
}
