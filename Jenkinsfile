
/*pipeline {
  agent none
  environment {
    APPLICATION_NAME = 'python-nginx'
    GIT_REPO="https://github.com/gawdamear/openshift-siebel-notes-sln.git"
    GIT_USER="1e948897-4d3a-40a2-aedd-53de8dd9f50b"
    GIT_BRANCH="master"
    PORT = 8081;
  }    
  stages {

    stage('Clone') {
      agent { label 'dotnetcore22' }
      steps {
        git credentialsId: "${GIT_USER}", branch: "${GIT_BRANCH}", url: "${GIT_REPO}"
      }
    }

    stage('Restore') {
      agent { label 'dotnetcore22' }
      steps {
        sh "dotnet restore"
      }
    }
    stage('Build Image') {
      agent { label 'base' }
      steps {
        script {
          openshift.withCluster() {
            openshift.withProject("peopleapi") {
              openshift.selector("bc", "peopleapi-bc").startBuild("--wait=true")
            }
          }
        }
      }
    }
    stage('Scan Codebase') {
      agent { label 'sonar-dotnet' }
      steps {
        withSonarQubeEnv('SonarQube-Server') {
          sh "dotnet sonarscanner begin /k:peopleapi /d:sonar.host.url=$SONAR_HOST_URL /d:sonar.login=$SONAR_AUTH_TOKEN"
          sh "dotnet build"
          sh "dotnet sonarscanner end /d:sonar.login=$SONAR_AUTH_TOKEN"
        }
      }
    }
    /*  You may need a docker-in-docker to run this on a Jenkins slave agent and do not like doing that */
    /*
    stage('Microscanner security scan'){
      steps {
        aquaMicroscanner imageName: 'peopleapi/peopleapi:latest', notCompliesCmd: 'exit 1', onDisallowed: 'fail', outputFormat: 'html'
      }
    }
    */
  //}
//}


pipeline {
  agent {
    node("dotnetcore22")
  }
  environment {
    APPLICATION_NAME = 'python-nginx'
    GIT_REPO="https://github.com/gawdamear/openshift-siebel-notes-sln.git"
    GIT_USER="1e948897-4d3a-40a2-aedd-53de8dd9f50b"
    GIT_BRANCH="master"
    PORT = 8081;
  }  

  stages {
    stage('Preamble') {
        steps {
            script {
                echo 'preambling...'
                git credentialsId: "${GIT_USER}", branch: "${GIT_BRANCH}", url: "${GIT_REPO}"
                sh "dotnet restore"
            }
        }
    }

    stage('Test') {
        steps {
            parallel (
                "Unit tests" : {
                    echo "unit testing..."
                },
                "Integration tests" : {
                    echo "integration testing..."
                }
            )
        } 
    }

    stage('Build Image') {
        steps {
            script {
                echo 'building image...'
            }
        }
    }  

    stage('Deploy') {
        steps {
            script {
                echo 'deploying image...'
            }
        }
    } 

    stage('System/Smoke test') {
        steps {
            script {
                echo 'sytem/smoke testing...'
            }
        }
    }         
  }
}