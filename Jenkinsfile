pipeline {
  agent { label 'dotnet-22' }

  environment {
    gitRepo ="https://github.com/gawdamear/openshift-siebel-notes-sln.git"
    gitUser ="dotnettest-github"
    gitBranch ="master"

    checkoutFolder = "/tmp/workspace/${env.JOB_NAME}"
    solutionName = "siebelnotes.sln"
  }    

  stages {
    stage('Checkout') {
      steps {
        git credentialsId: "${gitUser}", branch: "${gitBranch}", url: "${gitRepo}"
      }
    }    

    stage('Restore') {
      steps {
        dir(checkoutFolder) {
          sh "dotnet restore ${solutionName}"
        }
      }
    }

    stage('Clean') {
      steps {
        dir(checkoutFolder) {
          sh "dotnet clean ${solutionName}"
        }
      }
    }

    stage('Test') {
      steps {
          parallel (
              "Unit tests" : {
                  dir(checkoutFolder) {
                    sh "dotnet test ${solutionName}"
                  }
              },
              "Integration tests" : {
                  dir(checkoutFolder) {
                    echo "integration testing..."
                  }
              }
          )
      } 
    }
  }  
}

/* WORKING
node("dotnet-22") {

  environment {
    GIT_REPO="https://github.com/gawdamear/openshift-siebel-notes-sln.git"
    GIT_USER="dotnettest-github"
    GIT_BRANCH="master"
  }  

  stage('clone sources') {
      git credentialsId: "dotnettest-github", branch: "master", url: "https://github.com/gawdamear/openshift-siebel-notes-sln.git"
      //sh "git clone https://github.com/redhat-developer/s2i-dotnetcore-ex --branch dotnetcore-2.2 ."
  }

  stage('restore') {
    dir('app') {
      sh "dotnet restore ../siebelnotes.sln"
      //sh "dotnet publish -c Release /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App"
    }
  }

  stage('clean') {
    dir('app') {
      sh "dotnet clean ../siebelnotes.sln"
      //sh "dotnet publish -c Release /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App"
    }
  }

  stage('publish') {
    dir('app') {
      //sh "dotnet publish -c Release /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App"
    }
  }  
  
  stage('create image') {
    dir('app') {
      //sh 'oc new-build --name=dotnetapp dotnet:2.2 --binary=true || true'
      //sh 'oc start-build dotnetapp --from-dir=bin/Release/netcoreapp2.2/publish --follow'
    }
  }
}
*/

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

