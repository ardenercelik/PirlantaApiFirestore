 libraries {
     lib('arden')
 }
pipeline {
    agent any
    stages {
    stage ('Clean workspace') {
      steps {
        cleanWs()
      }
    }
    stage ('Git Checkout') {
      steps {
        git branch: 'test', credentialsId: 'arden-github', url: 'https://github.com/ardenercelik/PirlantaApiFirestore'
      }
    }
    stage('Restore packages') {
      steps {
        SayHello()
      }
    }
    }
}

def dotnet_build() {
    bat(script: 'dir && dotnet restore PirlantaApi.csproj &&dotnet build PirlantaApi.csproj', returnStdout: true)
}
