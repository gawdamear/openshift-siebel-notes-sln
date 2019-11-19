node('dotnet-22'){
   try { 

      def workingFolder = "/tmp/workspace/${env.JOB_NAME}"
      def publishArtifactFolder = "api/bin/Release/netcoreapp2.2/publish"
      def appStartUpProject = "api.csproj"
      def appStartUpProjectFolder = "api/$appStartUpProject"
      
      def openshiftImageName = 'siebel-notes-api'
      def openshiftProjectName = 'dotnet'

      def buildWithdotNetVersion = 'dotnet:2.2'
      
      openshift.withCluster() {
          stage('Checkout Source') {
            checkout()
          }   

          stage('Restore') {
            restore(workingFolder)
          } 

          stage('Clean') {
            clean(workingFolder)
          } 

          stage('Build Source') {
            build(workingFolder)
          }    

          stage('Testing') {
            parallel (
                "Unit tests" : {
                    unitTest(workingFolder)
                },
                "Integration tests" : {
                    integrationTest(workingFolder) 
                }
            )
          }
          
          stage('Build Image') {
             publishArtifact(workingFolder, appStartUpProjectFolder)
             binaryBuild(workingFolder, openshiftImageName, buildWithdotNetVersion, publishArtifactFolder)
          }
      }
    }
    finally {
      cleanUpWorkSpace()
    }     
}

def checkout (){
  checkout scm
}

def restore(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet restore"      
    }
}

def clean(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet clean --configuration Release"      
    }
}

def build(def workingFolder) {
    dir (workingFolder) {
      sh "dotnet build --configuration Release"      
    }
}

def unitTest(def workingFolder) {
    dir(workingFolder) {
      "dotnet test --logger:xunit"
      step([$class: 'XUnitBuilder', testTimeMargin: '3000', thresholdMode: 1, thresholds: [], 
      tools: [xUnitDotNet(deleteOutputFiles: true, failIfNotNew: false, 
      pattern: '**/TestResults/*.xml', skipNoTestFiles: false, stopProcessingIfError: true)]])   
    }
 }

def integrationTest(def workingFolder) {
    dir(workingFolder) {
      echo "integration testing..."
    }
}

def publishArtifact(def workingFolder, def appStartUpProjectFolder) {
    dir(workingFolder) {
      sh "dotnet publish $appStartUpProjectFolder -c Release /p:MicrosoftNETPlatformLibrary=Microsoft.NETCore.App"
    }
}

def binaryBuild(def workingFolder, def openshiftImageName, def buildWithdotNetVersion, def publishArtifactFolder) {
    dir(workingFolder) {
      sh "oc new-build --name=$openshiftImageName $buildWithdotNetVersion --binary=true"
      sh "oc start-build $openshiftImageName --from-dir=$publishArtifactFolder"
    }
}
def cleanUpWorkSpace(){
    cleanWs() 
}


